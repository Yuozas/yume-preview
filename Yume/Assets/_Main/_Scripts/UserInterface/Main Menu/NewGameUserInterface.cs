using SwiftLocator.Services.ServiceLocatorServices;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class NewGameUserInterface : MonoBehaviour
{
    private Character _selectedCharacter;
    private IRealmSaveManager _saveManager;
    private ISceneHelper _sceneHelper;
    private VisualElement _startContainer;
    private VisualElement _backToMenuContainer;
    private VisualElement _confirmStartContainer;
    private VisualElement _confirmationButtonContainer;

    void Awake()
    {
        _saveManager = ServiceLocator.SingletonProvider.Get<IRealmSaveManager>();
        _sceneHelper = ServiceLocator.GetSingleton<ISceneHelper>();

        var root = GetComponent<UIDocument>().rootVisualElement;
        var body = root.Q<VisualElement>("Body");
        _startContainer = body.Q<VisualElement>("GameStartContainer");
        _backToMenuContainer = body.Q<VisualElement>("BackToMenuContainer");
        _confirmStartContainer = body.Q<VisualElement>("ConfirmStartContainer");
        _confirmationButtonContainer = _confirmStartContainer.Q<VisualElement>("ConfirmationDialog").Q<VisualElement>("ConfirmationButtonContainer");

        SetupStartButton(Character.Ember);
        SetupStartButton(Character.Aura);
        SetupBackToMenuButton();
        SetupConfirmStartButton();
        SetupCancelStartButton();
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
        SceneManager.LoadScene(Scene.MainMenuScene.Name, LoadSceneMode.Single);
    }

    public void SetupConfirmStartButton()
    {
        var confirmStartButton = _confirmationButtonContainer.Q<Button>("ConfirmStartButton");
        confirmStartButton.clicked += TriggerConfirm;
    }

    public void TriggerConfirm()
    {
        _saveManager.CreateNewSave(_selectedCharacter);
        _sceneHelper.LoadActiveSaveScene();
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

    private void SetupStartButton(Character character)
    {
        var startButton = _startContainer.Query<Button>("StartButton").Build().Skip(Convert.ToInt32(character.Id)).First();

        startButton.text = character.Name;
        startButton.clicked += () =>
        {
            _selectedCharacter = character;
            TriggerStart();
        };
    }
}
