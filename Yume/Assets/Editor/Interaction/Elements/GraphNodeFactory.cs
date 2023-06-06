
using UnityEngine.UIElements;
using UnityEngine;
using System.Collections.Generic;
using UnityEditor.UIElements;

public class GraphNodeFactory
{
    public GraphNode Build(UnityNode unity)
    {
        var drawables = new List<IDrawable>()
        {
            new DrawableTitleContainer(unity.Type)
        };

        switch (unity.Type)
        {
            case INode.ENTRY:
                AddSingularOutputPortContainer(drawables);
                break;
            case INode.TYPEWRITER:
                AddTypewriterNodeElements(unity, drawables);
                break;
            case INode.NAME:
                AddNameNodeElements(unity, drawables);
                break;
            case INode.PORTRAIT:
                AddPortraitNodeElements(unity, drawables);
                break;
            case INode.ENABLE:
                AddEnableNodeElements(unity, drawables);
                break;
            case INode.DISABLE:
                AddDisableNodeElements(unity, drawables);
                break;
        }

        if (unity.Type != INode.ENTRY)
            AddSingularInputPortContainer(drawables);

        var array = drawables.ToArray();
        var node = new GraphNode(unity, array);
        return node;
    }

    private static void AddSingularInputPortContainer(List<IDrawable> drawables)
    {
        var singularInput = new SingularInputPortContainer();
        drawables.Add(singularInput);
    }

    private static void AddDisableNodeElements(UnityNode unity, List<IDrawable> drawables)
    {
        var executable = (DisableDialogueTogglerCommand)unity.Node.Executable;
        var dropdown = CreateTypeSelect(executable.Type, callback => executable.Type = callback.newValue);

        var extension = new DrawableExtensionContainer(dropdown);
        drawables.Add(extension);
    }

    private static void AddEnableNodeElements(UnityNode unity, List<IDrawable> drawables)
    {
        var executable = (EnableDialogueTogglerCommand)unity.Node.Executable;

        var dropdown = CreateTypeSelect(executable.Type, callback => executable.Type = callback.newValue);

        var extension = new DrawableExtensionContainer(dropdown);
        drawables.Add(extension);
    }

    private static void AddPortraitNodeElements(UnityNode unity, List<IDrawable> drawables)
    {
        var executable = (SetDialoguePortraitSettingsCommand)unity.Node.Executable;
        var faceField = CreateFaceFieldForPortrait(executable);
        var hairField = CreateHairFieldForPortrait(executable);

        var dropdown = CreateTypeSelect(executable.Type, callback => executable.Type = callback.newValue);

        var extension = new DrawableExtensionContainer(dropdown, faceField, hairField);
        drawables.Add(extension);
    }

    private static void AddNameNodeElements(UnityNode unity, List<IDrawable> drawables)
    {
        var executable = (SetDialogueNameSettingsCommand)unity.Node.Executable;
        var nameField = CreateNameFieldForName(executable);

        var dropdown = CreateTypeSelect(executable.Type, callback => executable.Type = callback.newValue);

        var extension = new DrawableExtensionContainer(dropdown, nameField);
        drawables.Add(extension);
    }

    private static void AddTypewriterNodeElements(UnityNode unity, List<IDrawable> drawables)
    {
        var executable = (ExecuteDialogueTypewriterCommand)unity.Node.Executable;

        var field = CreateTextFieldForTypewriter(executable);
        var rate = CreateFloatFieldForTypewriter(executable);
        var dropdown = CreateTypeSelect(executable.Type, callback => executable.Type = callback.newValue);

        var extension = new DrawableExtensionContainer(dropdown, rate, field);

        AddSingularOutputPortContainer(drawables);
        drawables.Add(extension);
    }

    private static void AddSingularOutputPortContainer(List<IDrawable> drawables)
    {
        var singularOutput = new SingularOutputPortContainer();
        drawables.Add(singularOutput);
    }

    private static DropdownField CreateTypeSelect(string type, EventCallback<ChangeEvent<string>> callback)
    {
        var dropdown = new DropdownField("Type", Dialogue.Types, Dialogue.Types.IndexOf(type));
        dropdown.RegisterValueChangedCallback(callback);

        return dropdown;
    }

    private static ObjectField CreateHairFieldForPortrait(SetDialoguePortraitSettingsCommand executable)
    {
        var hairField = new ObjectField("Hair")
        {
            objectType = typeof(Sprite),
            allowSceneObjects = false,
            value = executable.Settings.Hair
        };

        hairField.RegisterValueChangedCallback(callback => 
            executable.Settings = new PortraitSettings(executable.Settings.Face, callback.newValue as Sprite)
        );
        return hairField;
    }

    private static ObjectField CreateFaceFieldForPortrait(SetDialoguePortraitSettingsCommand executable)
    {
        var faceField = new ObjectField("Face")
        {
            objectType = typeof(Sprite),
            allowSceneObjects = false,
            value = executable.Settings.Face
        };

        faceField.RegisterValueChangedCallback(callback =>
            executable.Settings = new PortraitSettings(callback.newValue as Sprite, executable.Settings.Hair)
        );
        return faceField;
    }

    private static TextField CreateNameFieldForName(SetDialogueNameSettingsCommand executable)
    {
        var nameField = new TextField()
        {
            value = executable.Settings.Name,
            multiline = true
        };

        nameField.RegisterValueChangedCallback(callback =>
            executable.Settings = new NameSettings(callback.newValue)
        );
        return nameField;
    }

    private static FloatField CreateFloatFieldForTypewriter(ExecuteDialogueTypewriterCommand executable)
    {
        var rate = new FloatField("Rate")
        {
            value = executable.Settings.Rate
        };
        rate.RegisterValueChangedCallback(callback =>
            executable.Settings = new TypewriterSettings(executable.Settings.Sentence, executable.Settings.Rate)
        );

        return rate;
    }

    private static TextField CreateTextFieldForTypewriter(ExecuteDialogueTypewriterCommand executable)
    {
        var field = new TextField()
        {
            value = executable.Settings.Sentence,
            multiline = true
        };

        field.RegisterValueChangedCallback(callback =>
            executable.Settings = new TypewriterSettings(callback.newValue, executable.Settings.Rate)
        );

        return field;
    }
}
