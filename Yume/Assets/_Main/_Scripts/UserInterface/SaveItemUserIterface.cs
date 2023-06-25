using SwiftLocator.Services.ServiceLocatorServices;
using System;
using UnityEngine.UIElements;

public class SaveItemUserIterface
{
    private readonly Label _displayName;
    private readonly Label _dateTime;
    private readonly Button _continueButton;
    private readonly Button _deleteButton;
    private readonly RealmSaveDetails _realmSaveDetails;
    private readonly Action _onDelete;
    private readonly IRealmSaveManager _realmSaveManager;
    private readonly ISceneHelper _sceneHelper;

    public SaveItemUserIterface(VisualElement saveItem, RealmSaveDetails realmSaveDetails, Action onDelete)
    {
        var body = saveItem.Q<VisualElement>("Body");
        _displayName = body.Q<Label>("DisplayName");
        _dateTime = body.Q<Label>("DateTime");
        _continueButton = body.Q<Button>("ContinueButton");
        _deleteButton = body.Q<Button>("DeleteButton");

        _realmSaveDetails = realmSaveDetails;

        _onDelete = onDelete;
        SetupSaveItem();

        _realmSaveManager = ServiceLocator.GetSingleton<IRealmSaveManager>();
        _sceneHelper = ServiceLocator.GetSingleton<ISceneHelper>();
    }

    public void SetupSaveItem()
    {
        _displayName.text = _realmSaveDetails.DisplayName;
        _dateTime.text = _realmSaveDetails.Date.ToString("dd/MM/yyyy HH:mm:ss");
        _continueButton.clicked += TriggerContinue;
        _deleteButton.clicked += TriggerDelete;
    }

    private void TriggerDelete()
    {
        _realmSaveManager.DeleteSave(_realmSaveDetails.SaveId);
        _onDelete();
    }

    private void TriggerContinue()
    {
        _realmSaveManager.ChangeActiveSave(_realmSaveDetails.SaveId);
        _sceneHelper.LoadActiveSaveScene();
    }
}