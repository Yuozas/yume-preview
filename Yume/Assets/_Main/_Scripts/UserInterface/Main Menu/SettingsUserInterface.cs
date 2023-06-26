using SwiftLocator.Services.ServiceLocatorServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class SettingsUserInterface : MonoBehaviour
{
    private VisualElement _backToMenuContainer;

    private void Awake()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        var body = root.Q<VisualElement>("Body");
        _backToMenuContainer = body.Q<VisualElement>("BackToMenuContainer");

        SetupBackToMenuButton();
    }

    private void SetupBackToMenuButton()
    {
        var backToMenuButton = _backToMenuContainer.Q<Button>("BackToMenuButton");
        backToMenuButton.clicked += TriggerBack;
    }

    private void TriggerBack()
    {
        SceneManager.LoadScene(Scene.MainMenuScene.Name, LoadSceneMode.Single);
    }
}
