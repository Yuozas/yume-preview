using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InteractionGraphView : GraphView
{
    private readonly NodeFactory _nodeFactory;
    private readonly GraphNodeFactory _graphNodeFactory;

    private readonly List<GraphElement> _removables;
    private readonly List<GraphNode> _graphNodes;
    private readonly EditorWindow _window;

    private InteractionScriptableObject _interaction;
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

    public void Load(InteractionScriptableObject interaction)
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

    private void AssignNodeCreationRequest()
    {
        nodeCreationRequest = _interaction is not null ? OpenSearchWindow : null;
    }

    private GraphViewChange OnGraphChange(GraphViewChange change)
    {
        var edges = change.edgesToCreate;
        if (edges is not null)
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
        GetNodesAndIndex(edge, out var from, out var to, out var index);
        from.UnityNode.Node.Get(index).Add(to.UnityNode.Node);

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
                GetNodesAndIndex(edge, out var from, out var to, out var index);
                from.UnityNode.Node.Get(index).Remove(to.UnityNode.Node);
                break;
        }
    }

    private static void GetNodesAndIndex(Edge edge, out GraphNode from, out GraphNode to, out int index)
    {
        var output = edge.output;
        var input = edge.input;

        from = output.node as GraphNode;
        to = input.node as GraphNode;
        index = GetIndex(output, from);
    }

    private static int GetIndex(Port output, GraphNode from)
    {
        return from.outputContainer.Children()
            .Where(element => element is Port)
            .Cast<Port>()
            .ToList()
            .IndexOf(output);
    }

    private void CreateGraphNode(UnityNode unityNode)
    {
        var node = _graphNodeFactory.Build(this, unityNode);
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
            var connections = node.Connections;

            if (connections.Count <= 0)
                continue;

            var fromGraphNode = _graphNodes.First(n => n.UnityNode.Node == node);

            for (int i = 0; i < connections.Count; i++)
            {
                var nodes = connections[i].Nodes;
                if (nodes.Count <= 0)
                    continue;

                foreach (var toNode in nodes)
                {
                    var toGraphNode = _graphNodes.First(graphNode => graphNode.UnityNode.Node == toNode);
                    var toPort = (Port)toGraphNode.inputContainer.Children().First();
                    var fromPort = (Port)fromGraphNode.outputContainer.Children().ToList()[i];
                    var edge = fromPort.ConnectTo(toPort);

                    var contains = _removables.Contains(edge);
                    if (!contains)
                        _removables.Add(edge);

                    AddElement(edge);
                }
            }
        }
    }
}