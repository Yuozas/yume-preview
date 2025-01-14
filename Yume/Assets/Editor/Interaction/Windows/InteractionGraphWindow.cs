using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using UnityEngine;

public class InteractionGraphWindow : EditorWindow
{
    public const string TITLE = "Interaction";
    public const string PATH = "Tools/" + TITLE;

    private InteractionGraphView _view;
    private InteractionScriptableObject _interaction;

    private bool _autoLoad = true;

    [MenuItem(PATH)]
    public static void Open()
    {
        GetWindow<InteractionGraphWindow>(TITLE);
    }

    private void OnEnable()
    {
        CreateGraphView();
        CreateToolbar();

        if(_interaction is not null)
            _view.Load(_interaction);
    }

    private void CreateGraphView()
    {
        _view = new InteractionGraphView(this);
        _view.StretchToParentSize();

        rootVisualElement.Add(_view);
    }

    private void CreateToolbar()
    {
        var toolbar = new Toolbar();

        var field = new ObjectField()
        {
            objectType = typeof(InteractionScriptableObject),
            allowSceneObjects = false,
            value = _interaction
        };
        field.RegisterValueChangedCallback(HandleInteractionChanged);

        var loadButton = new Button(Load) { text = "Load" };
        var saveButton = new Button(Save) { text = "Save" };
        var clearButton = new Button(Clear) { text = "Clear" };
        var toggle = new Toggle("Auto-Load") { value = _autoLoad };

        toggle.RegisterValueChangedCallback(callback => _autoLoad = callback.newValue);

        toolbar.Add(field);
        toolbar.Add(loadButton);
        toolbar.Add(saveButton);
        toolbar.Add(clearButton);
        toolbar.Add(toggle);

        toolbar.StretchToParentSize();

        rootVisualElement.Add(toolbar);
    }

    private void HandleInteractionChanged(ChangeEvent<Object> @event)
    {
        _interaction = @event.newValue as InteractionScriptableObject;
        if(_interaction is null)
        {
            _view.Unload();
            return;
        }

        if (_autoLoad)
            Load();
    }

    private void Load()
    {
        if(_interaction is not null)
            _view.Load(_interaction);
    }

    private void Save()
    {
        if (_interaction is not null)
            _interaction.Save();
    }

    private void Clear()
    {
        if (_interaction is null)
            return;

        var confirmed = EditorUtility.DisplayDialog("Confirmation", "Are you sure? All nodes will be lost.", "Yes", "No");

        if (confirmed)
        {
            _interaction.Clear();
            Load();
        }
    }
}