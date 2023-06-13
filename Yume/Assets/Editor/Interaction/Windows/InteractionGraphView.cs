using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using System.Collections.Generic;
using System.Linq;

public class InteractionGraphView : GraphView
{
    private Interaction _interaction;
    private ContextualMenuManipulator _contextual;
    private readonly NodeFactory _nodeFactory;
    private readonly GraphNodeFactory _graphNodeFactory;

    private readonly List<GraphElement> _removables;
    private readonly List<GraphNode> _graphNodes;

    public InteractionGraphView()
    {
        graphViewChanged = OnGraphChange;
        _graphNodes = new();
        _nodeFactory = new NodeFactory(Dialogue.DEFAULT);
        _graphNodeFactory = new();
        _removables = new();

        AddBackground();
        AddStyling();
        AddManipulators();
    }

    private GraphViewChange OnGraphChange(GraphViewChange change)
    {
        var edges = change.edgesToCreate;
        if(edges is not null)
            foreach (var edge in edges)
                AddConnectionBasedOnEdge(edge);

        var removables = change.elementsToRemove;
        if (removables is not null)
            foreach (var element in change.elementsToRemove)
                CheckTypeAndRemoveIt(element);

        return change;
    }

    private void AddConnectionBasedOnEdge(Edge edge)
    {
        var from = (edge.output.node as GraphNode).UnityNode.Node;
        var to = (edge.input.node as GraphNode).UnityNode.Node;

        from.Add(to);

        var contains = _removables.Contains(edge);
        if (!contains)
            _removables.Add(edge);
    }

    private void CheckTypeAndRemoveIt(GraphElement element)
    {
        switch (element)
        {
            case GraphNode graphNode:
                _interaction.Remove(graphNode.UnityNode);
                break;
            case Edge edge:
                var from = (edge.output.node as GraphNode).UnityNode.Node;
                var to = (edge.input.node as GraphNode).UnityNode.Node;

                from.Remove(to);
                break;
        }
    }

    public void Load(Interaction interaction)
    {
        ClearElements();

        _interaction = interaction;
        AddContextualMenuManipulator();

        var nodes = _interaction.UnityNodes;

        foreach (var node in nodes)
            CreateGraphNode(node);

        DrawsConnectionLineBetweenNodes(nodes);
    }

    public void ClearElements()
    {
        _graphNodes.Clear();
        RemoveContextual();

        for (int i = _removables.Count - 1; i >= 0; i--)
        {
            var node = _removables[i];
            RemoveElement(node);
        }

        _removables.Clear();
    }

    public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
    {
        var updated = ports
            .Where(port => port.node != startPort.node && port.direction != startPort.direction
                && !port.connections.Any(edge => edge.output == startPort || edge.input == startPort))
            .ToList();

        return updated;
    }

    private void CreateGraphNode(UnityNode unityNode)
    {
        var node = _graphNodeFactory.Build(unityNode);
        node.Draw();
        AddElement(node);

        _graphNodes.Add(node);
        _removables.Add(node);
    }

    private void CreateNode(DropdownMenuAction action)
    {
        var node = _nodeFactory.Build(action.name);
        var unityNode = new UnityNode(node, action.eventInfo.localMousePosition);

        _interaction.Add(unityNode);
        CreateGraphNode(unityNode);
        AddContextualMenuManipulator();
    }

    private void AddStyling()
    {
        var variables = (StyleSheet)EditorGUIUtility.Load("Dialogue/DialogueVariables.uss");
        var viewStyle = (StyleSheet)EditorGUIUtility.Load("Dialogue/DialogueViewStyles.uss");

        styleSheets.Add(variables);
        styleSheets.Add(viewStyle);
    }

    private void AddBackground()
    {
        var background = new GridBackground();
        background.StretchToParentSize();

        Insert(0, background);
    }

    private void AddManipulators()
    {
        var contentDragger = new ContentDragger();
        var selector = new RectangleSelector();
        var selectionDragger = new SelectionDragger();

        this.AddManipulator(contentDragger);
        this.AddManipulator(selectionDragger);
        this.AddManipulator(selector);

        SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale * 2);
    }

    private void AddContextualMenuManipulator()
    {
        RemoveContextual();

        _contextual = new ContextualMenuManipulator(ContextualMenuBuilder);
        this.AddManipulator(_contextual);
    }

    private void RemoveContextual()
    {
        if (_contextual == null)
            return;
        this.RemoveManipulator(_contextual);
        _contextual = null;
    }

    private void ContextualMenuBuilder(ContextualMenuPopulateEvent @event)
    {
        @event.menu.AppendAction(INode.TYPEWRITER, CreateNode);
        @event.menu.AppendAction(INode.PORTRAIT, CreateNode);
        @event.menu.AppendAction(INode.NAME, CreateNode);
        @event.menu.AppendAction(INode.MUSIC, CreateNode);
        @event.menu.AppendAction(INode.ENABLE, CreateNode);
        @event.menu.AppendAction(INode.DISABLE, CreateNode);
        @event.menu.AppendAction(INode.SFX, CreateNode);

        var containsEntry = _interaction.Contains(INode.ENTRY);
        if (!containsEntry)
            @event.menu.AppendAction(INode.ENTRY, CreateNode);

        var containsExit = _interaction.Contains(INode.EXIT);
        if (!containsExit)
            @event.menu.AppendAction(INode.EXIT, CreateNode);
    }

    private void DrawsConnectionLineBetweenNodes(List<UnityNode> unityNodes)
    {
        foreach (var unityNode in unityNodes)
        {
            var node = unityNode.Node;
            if (node.Connections.Count <= 0)
                continue;

            var fromGraphNode = _graphNodes.First(n => n.UnityNode.Node == node);
            var fromPort = (Port)fromGraphNode.outputContainer.Children().First();

            foreach (var connection in node.Connections)
            {
                var toGraphNode = _graphNodes.First(node => node.UnityNode.Node == connection);
                var toPort = (Port)toGraphNode.inputContainer.Children().First();
                var edge = fromPort.ConnectTo(toPort);

                var contains = _removables.Contains(edge);
                if (!contains)
                    _removables.Add(edge);

                AddElement(edge);
            }
        }
    }
}
