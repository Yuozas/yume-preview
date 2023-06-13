using SwiftLocator.Services.ServiceLocatorServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class NewGameUserInterface : MonoBehaviour
{
    private ISaveManager _saveManager;
    private VisualElement _startContainer;
    private VisualElement _backToMenuContainer;
    private VisualElement _confirmStartContainer;
    private VisualElement _confirmationButtonContainer;

    void Awake()
    {
        _saveManager = ServiceLocator.SingletonProvider.Get<ISaveManager>();

        var root = GetComponent<UIDocument>().rootVisualElement;
        var body = root.Q<VisualElement>("Body");
        _startContainer = body.Q<VisualElement>("GameStartContainer");
        _backToMenuContainer = body.Q<VisualElement>("BackToMenuContainer");
        _confirmStartContainer = body.Q<VisualElement>("ConfirmStartContainer");
        _confirmationButtonContainer = _confirmStartContainer.Q<VisualElement>("ConfirmationDialog").Q<VisualElement>("ConfirmationButtonContainer");

        SetupStartButton();
        SetupBackToMenuButton();
        SetupConfirmStartButton();
        SetupCancelStartButton();
    }

    public void SetupStartButton()
    {
        var startButton = _startContainer.Query<Button>("StartButton").Build();
        foreach (var button in startButton)
            button.clicked += TriggerStart;
    }

    public void TriggerStart()
    {
        _startContainer.AddToClassList("hidden");
        _backToMenuContainer.AddToClassList("hidden");
        _confirmStartContainer.RemoveFromClassList("hidden");
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
    public void SetupConfirmStartButton()
    {
        var confirmStartButton = _confirmationButtonContainer.Q<Button>("ConfirmStartButton");
        confirmStartButton.clicked += TriggerConfirm;
    }

    public void TriggerConfirm()
    {
        //_saveManager.DeleteAllSaves();
        //_saveManager.CreateNewSave();
        //_saveManager.Save();
        //SceneLoader.LoadGameScene();
    }

    public void SetupCancelStartButton()
    {
        var cancelStartButton = _confirmationButtonContainer.Q<Button>("CancelStartButton");
        cancelStartButton.clicked += TriggerCancel;
    }

    public void TriggerCancel()
    {
        _startContainer.RemoveFromClassList("hidden");
        _backToMenuContainer.RemoveFromClassList("hidden");
        _confirmStartContainer.AddToClassList("hidden");
    }
}
