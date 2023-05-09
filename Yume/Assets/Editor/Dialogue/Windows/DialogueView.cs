using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using System;
using UnityEngine;

public class DialogueView : GraphView
{
    public DialogueView()
    {
        AddBackground();
        AddStyling();
        AddManipulators();
    }

    private void AddNode(Vector2 position)
    {
        var node = new DialogueNode(position);
        node.Draw();

        AddElement(node);
    }

    private void AddManipulators()
    {
        var contentDragger = new ContentDragger();
        var contextual = new ContextualMenuManipulator(ContextualMenuBuilder);
        var selector = new RectangleSelector();
        var selectionDragger = new SelectionDragger();

        this.AddManipulator(contentDragger);
        this.AddManipulator(contextual);
        this.AddManipulator(selectionDragger);
        this.AddManipulator(selector);

        SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);
    }

    private void ContextualMenuBuilder(ContextualMenuPopulateEvent @event)
    {
        @event.menu.AppendAction(DialogueNode.NAME, CreateDialogueNode);
    }

    private void CreateDialogueNode(DropdownMenuAction action)
    {
        AddNode(action.eventInfo.localMousePosition);
    }

    private void AddStyling()
    {
        var viewStyle = (StyleSheet)EditorGUIUtility.Load("Dialogue/DialogueViewStyles.uss");
        var nodeStyle = (StyleSheet)EditorGUIUtility.Load("Dialogue/DialogueNodeStyles.uss");

        styleSheets.Add(viewStyle);
        styleSheets.Add(nodeStyle);
    }

    private void AddBackground()
    {
        var background = new GridBackground();
        background.StretchToParentSize();

        Insert(0, background);
    }
}
