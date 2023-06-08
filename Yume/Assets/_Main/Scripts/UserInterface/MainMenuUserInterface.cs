using SwiftLocator.Services.ServiceLocatorServices;
using UnityEngine;
using UnityEngine.UIElements;

public class MainMenuUserInterface : MonoBehaviour
{
    private ISaveManager _saveManager;
    private VisualElement _buttonWrapper;

    void Awake()
    {
        _saveManager = ServiceLocator.SingletonProvider.Get<ISaveManager>();

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
        //var saveManager = ServiceLocator.SingletonProvider.Get<ISaveManager>();
        //saveManager.DeleteAllSaves();
        //SceneLoader.Load(SceneLoader.SceneName.Game);
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
        //await SceneManager.LoadSceneAsync();
    }

    private void SetupSettingsButton()
    {
        var settingsButton = _buttonWrapper.Q<Button>("SettingsButton");
        settingsButton.clicked += TriggerSettings;
    }

    private void TriggerSettings()
    {
        //SceneLoader.Load(SceneLoader.SceneName.Options);
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
