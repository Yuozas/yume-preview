using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

public class DialogueWindow : EditorWindow
{
    public const string TITLE = "Dialogue";
    public const string PATH = TITLE + "/Window";

    [MenuItem(PATH)]
    public static void Open()
    {
        GetWindow<DialogueWindow>(TITLE);
    }
    private void OnEnable()
    {
        CreateAndAddGraphView();
        CreateAndAddToolbar();
    }

    private void CreateAndAddToolbar()
    {
        var toolbar = new Toolbar();
        var field = new TextField
        {
            label = "File:",
            value = "Shop"
        };

        var button = new Button
        {
            text = "Save"
        };

        toolbar.Add(field);
        toolbar.Add(button);

        toolbar.StretchToParentSize();

        rootVisualElement.Add(toolbar);
    }

    private void CreateAndAddGraphView()
    {
        var view = new DialogueView();
        view.StretchToParentSize();

        rootVisualElement.Add(view);
    }
}