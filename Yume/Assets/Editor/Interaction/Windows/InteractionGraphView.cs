using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InteractionGraphView : GraphView
{
    private Interaction _interaction;
    private ContextualMenuManipulator _contextual;
    private readonly NodeFactory _nodeFactory;
    private readonly GraphNodeFactory _graphNodeFactory;

    private readonly List<GraphElement> _removables;
    private readonly List<GraphNode> _graphNodes;
    private readonly EditorWindow _window;

    private InteractionSearchWindow _search;

    public InteractionGraphView(EditorWindow window)
    {
        graphViewChanged = OnGraphChange;
        _graphNodes = new();
        _nodeFactory = new NodeFactory(Dialogue.DEFAULT);
        _graphNodeFactory = new();
        _removables = new();
        _window = window;

        CreateAndAddSearchWindow();
        AddBackground();
        AddStyling();
        AddManipulators();
        AssignNodeCreationRequest();
    }

    private void AssignNodeCreationRequest()
    {
        nodeCreationRequest = _interaction is not null ? OpenSearchWindow : null;
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
        Unload();

        _interaction = interaction;

        var nodes = _interaction.UnityNodes;

        foreach (var node in nodes)
            CreateGraphNode(node);

        DrawsConnectionLineBetweenNodes(nodes);
        AssignNodeCreationRequest();
    }

    public void Unload()
    {
        _graphNodes.Clear();

        foreach (var removable in _removables)
            RemoveElement(removable);

        _removables.Clear();
        _interaction = null;

        AssignNodeCreationRequest();
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

    private void CreateNode(string type, SearchWindowContext context)
    {
        var node = _nodeFactory.Build(type);
        var position = _window.rootVisualElement.ChangeCoordinatesTo(_window.rootVisualElement.parent, context.screenMousePosition - _window.position.position);
        var local = contentViewContainer.WorldToLocal(position);
        var unityNode = new UnityNode(node, local);

        _interaction.Add(unityNode);
        CreateGraphNode(unityNode);
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

    private void CreateAndAddSearchWindow()
    {
        _search = _search is null ? ScriptableObject.CreateInstance<InteractionSearchWindow>() : _search;
        _search.Initialize(CreateNode);
    }

    private void OpenSearchWindow(NodeCreationContext context)
    {
        var search = new SearchWindowContext(context.screenMousePosition);
        SearchWindow.Open(search, _search);
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