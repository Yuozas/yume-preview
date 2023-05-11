using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using UnityEngine;

public class DialogueNode : Node
{
    public const string NAME = "Dialogue";
    public const string IN_PORT_NAME = "In";
    public const string OUT_PORT_NAME = "Out";
    public const string DIALOGUE_TEXT = "Text";

    public DialogueNode(Vector2 position)
    {
        var rect = new Rect(position, Vector3.zero);
        SetPosition(rect);
    }

    public void Draw()
    {
        var title = new Label("Dialogue Node");
        titleContainer.Insert(0, title);


        DrawInAndOutPort();
        DrawDialogueText();
    }

    private void DrawInAndOutPort()
    {
        var type = typeof(bool);

        var input = InstantiatePort(Orientation.Horizontal, UnityEditor.Experimental.GraphView.Direction.Input, Port.Capacity.Multi, type);
        input.portName = IN_PORT_NAME;

        var output = InstantiatePort(Orientation.Horizontal, UnityEditor.Experimental.GraphView.Direction.Output, Port.Capacity.Single, type);
        output.portName = OUT_PORT_NAME;

        inputContainer.Add(input);
        outputContainer.Add(output);
    }

    private void DrawDialogueText()
    {
        var element = new VisualElement();
        var foldout = new Foldout()
        {
            text = NAME
        };

        var field = new TextField()
        {
            value = DIALOGUE_TEXT
        };

        foldout.Add(field);
        element.Add(foldout);

        extensionContainer.Add(element);
        RefreshExpandedState();
    }
}