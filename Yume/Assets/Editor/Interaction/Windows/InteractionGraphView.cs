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
        if(edges != null)
        {
            foreach (var edge in edges)
            {
                var from = (edge.output.node as GraphNode).UnityNode.Node;
                var to = (edge.input.node as GraphNode).UnityNode.Node;

                from.Add(to);

                var contains = _removables.Contains(edge);
                if(!contains)
                    _removables.Add(edge);
            }
        }

        var removables = change.elementsToRemove;
        if (removables != null)
        {
            foreach (var element in change.elementsToRemove)
            {
                var type = typeof(GraphNode);
                if (element.GetType() == type)
                {
                    var converted = (GraphNode)element;
                    _interaction.Remove(converted.UnityNode);
                }
                else if(element.GetType() == typeof(Edge))
                {
                    var converted = (Edge)element;
                    var from = (converted.output.node as GraphNode).UnityNode.Node;
                    var to = (converted.input.node as GraphNode).UnityNode.Node;

                    from.Remove(to);
                }
            }
        }

        return change;
    }

    public void Load(Interaction interaction)
    {
        ClearElements();

        _interaction = interaction;
        AddContextualMenuManipulator();

        var unityNodes = _interaction.UnityNodes;
        for (int i = 0; i < unityNodes.Count; i++)
        {
            var node = unityNodes[i];
            CreateGraphNode(node);
        }

        for (int i = 0; i < unityNodes.Count; i++)
        {
            var unityNode = unityNodes[i].Node;
            if (unityNode.Connections.Count <= 0) continue;

            var fromGraphNode = _graphNodes.First(node => node.UnityNode.Node == unityNode);
            var fromPort = (Port)fromGraphNode.outputContainer.Children().First();

            for (int j = 0; j < unityNode.Connections.Count; j++)
            {
                var toNode = unityNode.Connections[j];

                var toGraphNode = _graphNodes.First(node => node.UnityNode.Node == toNode);
                var toPort = (Port)toGraphNode.inputContainer.Children().First();
                var edge = fromPort.ConnectTo(toPort);


                var contains = _removables.Contains(edge);
                if(!contains)
                    _removables.Add(edge);
                AddElement(edge);
            }
        }
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
        @event.menu.AppendAction(INode.ENABLE, CreateNode);
        @event.menu.AppendAction(INode.DISABLE, CreateNode);

        var containsEntry = _interaction.Contains(INode.ENTRY);
        if (!containsEntry)
            @event.menu.AppendAction(INode.ENTRY, CreateNode);

        var containsExit = _interaction.Contains(INode.EXIT);
        if (!containsExit)
            @event.menu.AppendAction(INode.EXIT, CreateNode);
    }
}
