using SwiftLocator.Services.ServiceLocatorServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenuUserInterface : MonoBehaviour
{
    private VisualElement _buttonWrapper;
    private IRealmSaveReadHelper _realmSaveReadHelper;

    private void Awake()
    {
        _realmSaveReadHelper = ServiceLocator.GetSingleton<IRealmSaveReadHelper>(); 

        var root = GetComponent<UIDocument>().rootVisualElement;
        var body = root.Q<VisualElement>("Body");
        _buttonWrapper = body.Q<VisualElement>("MenuButtonsWrapper");

        SetupContinueButton();
        SetupNewGameButton();
        SetupSettingsButton();
        SetupQuitGameButton();
    }

    private void SetupNewGameButton()
    {
        var newGameButton = _buttonWrapper.Q<Button>("NewGameButton");
        newGameButton.clicked += TriggerNewGame;
    }

    private void TriggerNewGame()
    {
        SceneManager.LoadScene(Scene.NewGameScene.Name, LoadSceneMode.Single);
    }

    private void SetupContinueButton()
    {
        var continueButton = _buttonWrapper.Q<Button>("ContinueButton");
        if (_realmSaveReadHelper.AnySaveExists())
        {
            continueButton.clicked += TriggerContinue;
            return;
        }
        continueButton.SetEnabled(false);
        continueButton.AddToClassList("button-hidden");
    }

    private void TriggerContinue()
    {
        SceneManager.LoadScene(Scene.ContinueScene.Name, LoadSceneMode.Single);
    }

    private void SetupSettingsButton()
    {
        var settingsButton = _buttonWrapper.Q<Button>("SettingsButton");
        settingsButton.clicked += TriggerSettings;
    }

    private void TriggerSettings()
    {
        SceneManager.LoadScene(Scene.SettingsScene.Name, LoadSceneMode.Single);
    }

    private void SetupQuitGameButton()
    {
        var quitButton = _buttonWrapper.Q<Button>("QuitGameButton");
        quitButton.clicked += TriggerQuitGame;
    }

    private void TriggerQuitGame()
    {
        Application.Quit();
    }
}
