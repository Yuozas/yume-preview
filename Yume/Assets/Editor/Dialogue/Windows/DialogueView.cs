using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using System;
using UnityEngine;
using System.Collections.Generic;

public class DialogueView : GraphView
{
    public DialogueView()
    {
        var options = new OptionsNode();
        options.Draw();

        AddBackground();
        AddStyling();
        AddManipulators();
    }

    public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
    {
        var compatible = new List<Port>();
        foreach (var port in ports)
        {
            if (port.node == startPort.node)
                continue;
            if (port.direction == startPort.direction)
                continue;

            compatible.Add(port);
        }

        return compatible;
    }

    private void AddNode(Vector2 position)
    {
        var node = new DialogueNode(position);
        node.Draw();

        AddElement(node);
    }
    private void AddEntry(Vector2 position)
    {
        var node = new EntryNode(position);
        node.Draw();

        AddElement(node);
    }
    private void AddExit(Vector2 position)
    {
        var node = new ExitNode(position);
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

        SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale * 2);
    }

    private void ContextualMenuBuilder(ContextualMenuPopulateEvent @event)
    {
        @event.menu.AppendAction(DialogueNode.NAME, CreateDialogueNode);
        @event.menu.AppendAction(EntryNode.NAME, CreateEntryNode);
        @event.menu.AppendAction(ExitNode.NAME, CreateExitNode);
    }

    private void CreateDialogueNode(DropdownMenuAction action)
    {
        AddNode(action.eventInfo.localMousePosition);
    }

    private void CreateEntryNode(DropdownMenuAction action)
    {
        AddEntry(action.eventInfo.localMousePosition);
    }
    private void CreateExitNode(DropdownMenuAction action)
    {
        AddExit(action.eventInfo.localMousePosition);
    }
    private void AddStyling()
    {
        var viewStyle = (StyleSheet)EditorGUIUtility.Load("Dialogue/DialogueViewStyles.uss");
        styleSheets.Add(viewStyle);
    }

    private void AddBackground()
    {
        var background = new GridBackground();
        background.StretchToParentSize();

        Insert(0, background);
    }
}
