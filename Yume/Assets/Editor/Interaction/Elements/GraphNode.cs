using UnityEngine.UIElements;
using UnityEngine;
using UnityEditor;
using Node = UnityEditor.Experimental.GraphView.Node;
using System.Collections.Generic;

public class GraphNode : Node
{
    public readonly UnityNode UnityNode;
    private readonly List<IDrawable> _drawables;

    private const string STYLES_PATH = "Dialogue/DialogueNodeStyles.uss";


    public GraphNode(UnityNode node, params IDrawable[] drawables)
    {
        UnityNode = node;
        _drawables = new List<IDrawable>();

        AddDrawables(drawables);

        AddStylesheet();
        SetPosition(node.Position);
    }

    public void Draw()
    {
        foreach (var drawable in _drawables)
            drawable.Draw();

        RefreshExpandedState();
        RefreshPorts();
    }

    public override void SetPosition(Rect position)
    {
        base.SetPosition(position);
        UnityNode.SetPosition(position.position);
    }

    private void AddDrawables(params IDrawable[] drawables)
    {
        for (int i = 0; i < drawables.Length; i++)
        {
            var drawable = drawables[i];
            drawable.Set(this);
            _drawables.Add(drawable);
        }
    }

    private void SetPosition(Vector2 position)
    {
        var rect = new Rect(position, Vector3.zero);
        SetPosition(rect);
    }
    private void AddStylesheet()
    {
        var nodeStyle = (StyleSheet)EditorGUIUtility.Load(STYLES_PATH);
        styleSheets.Add(nodeStyle);
    }
}
