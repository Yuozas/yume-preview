using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using UnityEngine;
using UnityEditor;

public class DialogueNode : Node
{
    public const string NAME = "Node";
    public const string IN_PORT_NAME = "In";
    public const string OUT_PORT_NAME = "Out";
    public const string DIALOGUE_TEXT = "Text";

    public DialogueNode(Vector2 position)
    {
        var rect = new Rect(position, Vector3.zero);
        SetPosition(rect);

        var nodeStyle = (StyleSheet)EditorGUIUtility.Load("Dialogue/DialogueNodeStyles.uss");
        styleSheets.Add(nodeStyle);
    }

    public void Draw()
    {
        var title = new Label(NAME);
        title.AddToClassList("title-element");

        titleContainer.Insert(0, title);

        DrawInAndOutPort();
        DrawDialogueText();
    }

    private void DrawInAndOutPort()
    {
        var type = typeof(bool);

        ColorUtility.TryParseHtmlString("#84E4E7", out var color);

        var input = InstantiatePort(Orientation.Horizontal, UnityEditor.Experimental.GraphView.Direction.Input, Port.Capacity.Multi, type);
        input.portName = IN_PORT_NAME + "(Any)";
        input.portColor = color;

        var output = InstantiatePort(Orientation.Horizontal, UnityEditor.Experimental.GraphView.Direction.Output, Port.Capacity.Single, type);
        output.portName = OUT_PORT_NAME + "(1)";
        output.portColor = color;

        inputContainer.Add(input);
        outputContainer.Add(output);

        RefreshPorts();
    }

    private void DrawDialogueText()
    {
        var element = new VisualElement();
        var foldout = new Foldout();
        foldout.AddToClassList("foldout-element");

        var field = new TextField()
        {
            value = DIALOGUE_TEXT,
            multiline = true
        };

        field.AddToClassList("foldout-text-element");


        var content = foldout.contentContainer;
        content.AddToClassList("foldout-content");
        content.Add(field);

        element.Add(foldout);

        extensionContainer.Add(element);
        extensionContainer.AddToClassList("extension-container");
        RefreshExpandedState();
    }
}

public class EntryNode : Node
{
    public const string NAME = "Entry";
    public const string OUT_PORT_NAME = "Out";

    public EntryNode(Vector2 position)
    {
        var rect = new Rect(position, Vector3.zero);
        SetPosition(rect);

        var nodeStyle = (StyleSheet)EditorGUIUtility.Load("Dialogue/DialogueNodeStyles.uss");
        styleSheets.Add(nodeStyle);
    }

    public void Draw()
    {
        var title = new Label(NAME);
        title.AddToClassList("title-element");

        titleContainer.Insert(0, title);

        DrawOutPort();
    }

    private void DrawOutPort()
    {
        var type = typeof(bool);

        ColorUtility.TryParseHtmlString("#84E4E7", out var color);

        var output = InstantiatePort(Orientation.Horizontal, UnityEditor.Experimental.GraphView.Direction.Output, Port.Capacity.Single, type);
        output.portName = OUT_PORT_NAME + "(1)";
        output.portColor = color;

        outputContainer.Add(output);

        RefreshPorts();
    }
}

public class ExitNode : Node
{
    public const string NAME = "Exit";
    public const string IN_PORT_NAME = "In";

    public ExitNode(Vector2 position)
    {
        var rect = new Rect(position, Vector3.zero);
        SetPosition(rect);

        var nodeStyle = (StyleSheet)EditorGUIUtility.Load("Dialogue/DialogueNodeStyles.uss");
        styleSheets.Add(nodeStyle);
    }

    public void Draw()
    {
        var title = new Label(NAME);
        title.AddToClassList("title-element");

        titleContainer.Insert(0, title);

        DrawInPort();
    }

    private void DrawInPort()
    {
        var type = typeof(bool);

        ColorUtility.TryParseHtmlString("#84E4E7", out var color);

        var input = InstantiatePort(Orientation.Horizontal, UnityEditor.Experimental.GraphView.Direction.Input, Port.Capacity.Multi, type);
        input.portName = IN_PORT_NAME + "(Any)";
        input.portColor = color;

        inputContainer.Add(input);

        RefreshPorts();
    }
}
