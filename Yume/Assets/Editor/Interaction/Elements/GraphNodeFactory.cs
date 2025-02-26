﻿using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using UnityEngine;
using System.Collections.Generic;
using UnityEditor.UIElements;
using System.Linq;

public class GraphNodeFactory
{
    public GraphNode Build(GraphView view, UnityNode unity)
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
            case INode.MUSIC:
                AddMusicNodeElements(unity, drawables);
                break;
            case INode.ENABLE:
                AddEnableNodeElements(unity, drawables);
                break;
            case INode.DISABLE:
                AddDisableNodeElements(unity, drawables);
                break;
            case INode.PLAY_SOUND_EFFECT:
                AddSfxNodeElements(unity, drawables);
                break;
            case INode.SET_DECISION_CHOICES:
                AddSetChoicesNodeElements(unity, drawables, view);
                break;
            case INode.TRANSITION_TO_DESTINATION:
                AddTransitionToDestinationNodeElements(unity, drawables);
                break;
            case INode.PLAY_SLIDER_GAME:
                AddSliderGameNodeElements(unity, drawables);
                break;
            case INode.WAIT:
                AddWaitNodeElements(unity, drawables, view);
                break;
            case INode.INVOKE_SCRIPTABLE_OBJECT_EVENT:
                AddScriptableObjectEventNodeElements(unity, drawables);
                break;
            case INode.ADD_QUEST:
                AddQuestNodeElements(unity, drawables);
                break;
            case INode.OPEN_SHOP:
                AddOpenShopNodeElements(unity, drawables);
                break;
        }

        if (unity.Type != INode.ENTRY)
            AddSingularInputPortContainer(drawables);

        var array = drawables.ToArray();
        var node = new GraphNode(unity, array);
        return node;
    }

    private static void AddMusicNodeElements(UnityNode unity, List<IDrawable> drawables)
    {
        var executable = (PlayMusicClipCommand)unity.Node.Executable;

        var floatField = new FloatField("Cross Fade Duration")
        {
            value = executable.Settings.CrossFadeDuration
        };
        floatField.RegisterValueChangedCallback(callback =>
            executable.Settings = new MusicClipSettings(executable.Settings.Clip, callback.newValue)
        );

        var clipField = CreateField("Music", executable.Settings.Clip);
        clipField.RegisterValueChangedCallback(callback =>
            executable.Settings = new MusicClipSettings(callback.newValue as AudioClip, executable.Settings.CrossFadeDuration)
        );

        var extension = new DrawableExtensionContainer(floatField, clipField);
        drawables.Add(extension);
    }

    private static void AddSfxNodeElements(UnityNode unity, List<IDrawable> drawables)
    {
        var executable = (PlaySoundEffectClipCommand)unity.Node.Executable;

        var clipField = CreateField("Sfx", executable.Settings.Clip);
        clipField.RegisterValueChangedCallback(callback =>
            executable.Settings = new SoundEffectClipSettings(callback.newValue as AudioClip, executable.Settings.VolumeScale)
        );

        var floatField = new FloatField("Volume Scale") { value = executable.Settings.VolumeScale };
        floatField.RegisterValueChangedCallback(callback =>
            executable.Settings = new SoundEffectClipSettings(executable.Settings.Clip, callback.newValue)
        );

        var extension = new DrawableExtensionContainer(clipField, floatField);
        drawables.Add(extension);
    }

    private static void AddSingularInputPortContainer(List<IDrawable> drawables)
    {
        var singularInput = new SingularPortContainer(SingularPortContainer.IN_PORT_NAME, Direction.Input);
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

        var dropdown = CreateTypeSelect(executable.Type, callback => executable.Type = callback.newValue, Dialogue.INSPECTION);

        var extension = new DrawableExtensionContainer(dropdown, faceField, hairField);
        drawables.Add(extension);
    }

    private static void AddNameNodeElements(UnityNode unity, List<IDrawable> drawables)
    {
        var executable = (SetDialogueNameSettingsCommand)unity.Node.Executable;
        var nameField = CreateNameFieldForName(executable);

        var dropdown = CreateTypeSelect(executable.Type, callback => executable.Type = callback.newValue, Dialogue.INSPECTION);

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

    private void AddSetChoicesNodeElements(UnityNode unity, List<IDrawable> drawables, GraphView view)
    {
        var compositeOutput = new MultiplePortContainer(Direction.Output, unity.Node.Connections, view);
        var button = new Button() { text = "Add Choice" };
        button.clicked += () =>
        {
            var connection = unity.Node.AddConnection();
            compositeOutput.CreateAndAddPort(connection, true);
        };

        var extension = new DrawableExtensionContainer(button);

        drawables.Add(compositeOutput);
        drawables.Add(extension);
    }

    private void AddSliderGameNodeElements(UnityNode unity, List<IDrawable> drawables)
    {
        var compositeOutput = new BoolOutputPortContainer(unity.Node);
        drawables.Add(compositeOutput);
    }
    
    private void AddScriptableObjectEventNodeElements(UnityNode unity, List<IDrawable> drawables)
    {
        var executable = (InvokeScriptableObjectEventCommand)unity.Node.Executable;
        var field = CreateField("Event", executable.EventScriptableObject);

        field.RegisterValueChangedCallback(callback =>
            executable.EventScriptableObject = (EventScriptableObject)callback.newValue
        );

        var extension = new DrawableExtensionContainer(field);

        drawables.Add(extension);
    }
    
    private void AddQuestNodeElements(UnityNode unity, List<IDrawable> drawables)
    {
        var executable = (AddQuestCommand)unity.Node.Executable;
        var field = CreateField("Quest", executable.QuestScriptableObject);

        field.RegisterValueChangedCallback(callback =>
            executable.QuestScriptableObject = (QuestScriptableObject)callback.newValue
        );

        var extension = new DrawableExtensionContainer(field);

        drawables.Add(extension);
    }

    private void AddOpenShopNodeElements(UnityNode unity, List<IDrawable> drawables)
    {
        var executable = (OpenShopCommand)unity.Node.Executable;
        var field = CreateField("Store", executable.StoreScriptableObject);

        field.RegisterValueChangedCallback(callback =>
            executable.StoreScriptableObject = (StoreScriptableObject)callback.newValue
        );

        var extension = new DrawableExtensionContainer(field);

        drawables.Add(extension);
    }

    private void AddWaitNodeElements(UnityNode unity, List<IDrawable> drawables, GraphView view)
    {
        var executable = (WaitCommand)unity.Node.Executable;
        var field = new FloatField("Duration") { value = executable.Duration };

        field.RegisterValueChangedCallback(callback =>
            executable.Duration = callback.newValue
        );

        AddSingularOutputPortContainer(drawables);
        var extension = new DrawableExtensionContainer(field);

        drawables.Add(extension);
    }

    private void AddTransitionToDestinationNodeElements(UnityNode unity, List<IDrawable> drawables)
    {
        var executable = (TransitionToDestinationCommand)unity.Node.Executable;
        var field = CreateField("Scriptable", executable.DestinationScriptableObject);
        field.RegisterValueChangedCallback(callback => executable.DestinationScriptableObject = (TransitionDestinationScriptableObject)callback.newValue);

        var extension = new DrawableExtensionContainer(field);

        AddSingularOutputPortContainer(drawables);
        drawables.Add(extension);
    }

    private static void AddSingularOutputPortContainer(List<IDrawable> drawables)
    {
        var singularOutput = new SingularPortContainer(SingularPortContainer.OUT_PORT_NAME, Direction.Output);
        drawables.Add(singularOutput);
    }

    private static DropdownField CreateTypeSelect(string type, EventCallback<ChangeEvent<string>> callback, params string[] discludes)
    {
        var types = Dialogue.Types.Except(discludes).ToList();
        var index = Dialogue.Types.IndexOf(type);

        var dropdown = new DropdownField("Type", types, index);
        dropdown.RegisterValueChangedCallback(callback);

        return dropdown;
    }

    private static ObjectField CreateHairFieldForPortrait(SetDialoguePortraitSettingsCommand executable)
    {
        var field = CreateField("Hair", executable.Settings.Hair);

        field.RegisterValueChangedCallback(callback =>
            executable.Settings = new PortraitSettings(executable.Settings.Face, callback.newValue as Sprite)
        );
        return field;
    }

    private static ObjectField CreateFaceFieldForPortrait(SetDialoguePortraitSettingsCommand executable)
    {
        var field = CreateField("Face", executable.Settings.Face);

        field.RegisterValueChangedCallback(callback =>
            executable.Settings = new PortraitSettings(callback.newValue as Sprite, executable.Settings.Hair)
        );
        return field;
    }

    private static ObjectField CreateField<T>(string name, T value) where T : Object
    {
        return new ObjectField(name)
        {
            objectType = typeof(T),
            allowSceneObjects = false,
            value = value
        };
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
            executable.Settings = new TypewriterSettings(executable.Settings.Sentence, callback.newValue)
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
