using SwiftLocator.Services.ServiceLocatorServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class SettingsUserInterface : MonoBehaviour
{
    private VisualElement _backToMenuContainer;
    private SceneDataHandler _sceneDataHandler;

    private void Awake()
    {
        _sceneDataHandler = ServiceLocator.GetSingleton<SceneDataHandler>();

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
        SceneManager.LoadScene(_sceneDataHandler.MainMenuSceneName, LoadSceneMode.Single);
    }
}
