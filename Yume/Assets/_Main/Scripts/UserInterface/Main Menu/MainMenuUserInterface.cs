using SwiftLocator.Services.ServiceLocatorServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenuUserInterface : MonoBehaviour
{
    private IRealmSaveManager _saveManager;
    private SceneDataHandler _sceneDataHandler;
    private VisualElement _buttonWrapper;

    void Awake()
    {
        _saveManager = ServiceLocator.SingletonProvider.Get<IRealmSaveManager>();
        _sceneDataHandler = ServiceLocator.SingletonProvider.Get<SceneDataHandler>();

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
        SceneManager.LoadScene(_sceneDataHandler.NewGameSceneName, LoadSceneMode.Single);
    }

    private void SetupContinueButton()
    {
        var continueButton = _buttonWrapper.Q<Button>("ContinueButton");
        
        if (!_saveManager.AnySaveExists())
        {
            continueButton.SetEnabled(false);
            continueButton.AddToClassList("button-hidden");
        }
        else
        {
            continueButton.clicked += TriggerContinue;
        }
    }

    private void TriggerContinue()
    {
        SceneManager.LoadScene(_sceneDataHandler.ContinueSceneName, LoadSceneMode.Single);
    }

    private void SetupSettingsButton()
    {
        var settingsButton = _buttonWrapper.Q<Button>("SettingsButton");
        settingsButton.clicked += TriggerSettings;
    }

    private void TriggerSettings()
    {
        SceneManager.LoadScene(_sceneDataHandler.SettingsSceneName, LoadSceneMode.Single);
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
