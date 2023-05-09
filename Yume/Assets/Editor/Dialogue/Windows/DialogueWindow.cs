using UnityEditor;
using UnityEngine.UIElements;


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
        var view = new DialogueView();
        view.StretchToParentSize();

        rootVisualElement.Add(view);
    }
}