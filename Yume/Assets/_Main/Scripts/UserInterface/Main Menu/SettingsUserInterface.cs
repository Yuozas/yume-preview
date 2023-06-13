using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class SettingsUserInterface : MonoBehaviour
{
    private VisualElement _backToMenuContainer;

    public void Awake()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        var body = root.Q<VisualElement>("Body");
        _backToMenuContainer = body.Q<VisualElement>("BackToMenuContainer");

        SetupBackToMenuButton();
    }

    public void SetupBackToMenuButton()
    {
        var backToMenuButton = _backToMenuContainer.Q<Button>("BackToMenuButton");
        backToMenuButton.clicked += TriggerBack;
    }

    public void TriggerBack()
    {
        SceneManager.LoadScene(SceneData.MainMenuSceneName, LoadSceneMode.Single);
    }
}
