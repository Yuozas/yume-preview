using SwiftLocator.Services.ServiceLocatorServices;
using System.Linq;
using UnityEditor;
using UnityEngine.UIElements;

public class RealmDebugigngWindow : EditorWindow
{
    [MenuItem("Tools/Realm/Debugging")]
    public static void Open()
    {
        GetWindow<RealmDebugigngWindow>("Setup debugging");
    }

    private void OnEnable()
    {
        AddDescription();
        AddSceneDropdownField();
        AddSaveIdIntegerField();
    }

    private void AddDescription()
    {
        var description = new Label("If fields are empty no debugging values will be used.");
        description.style.marginTop = 10;
        description.style.marginBottom = 10;
        rootVisualElement.Add(description);
    }

    private void AddSceneDropdownField()
    {
        var allScenes = ServiceLocator.GetSingleton<ISceneHelper>().GetAllSceneNames().ToList();
        var defaultIndex = 0;

        using var realm = ServiceLocator.GetSingleton<IRealmContext>().GetGlobalRealm();
        if (realm.TryGet<DebuggingRealm>(out var debuggingRealm))
            defaultIndex = allScenes.IndexOf(debuggingRealm.SceneName);

        var dropdownField = new DropdownField("Scene name", allScenes, defaultIndex);
        
        dropdownField.RegisterValueChangedCallback(OnDebuggingSceneNameChanged);

        rootVisualElement.Add(dropdownField);
    }

    private void OnDebuggingSceneNameChanged(ChangeEvent<string> evt)
    {
        using var realm = ServiceLocator.GetSingleton<IRealmContext>().GetGlobalRealm();
        if (realm.TryGet<DebuggingRealm>(out var debuggingRealm))
            realm.Write(() =>
            {
                debuggingRealm.SceneName = evt.newValue;
                realm.Add(debuggingRealm, true);
            });
        else
            realm.WriteAdd(new DebuggingRealm { SceneName = evt.newValue });
    }

    private void AddSaveIdIntegerField()
    {
        var allSaveIds = ServiceLocator.GetSingleton<IRealmSaveReadHelper>()
            .GetAllSaveDetails()
            .Select(save => save.SaveId.ToString())
            .ToList();

        var defaultIndex = 0;
        using var activeSaveDetails = ServiceLocator.GetSingleton<IRealmActiveSaveHelper>().GetActiveSaveDetails();
        var index = allSaveIds.IndexOf(activeSaveDetails.Result.SaveId.ToString());
        if(index is not -1)
            defaultIndex = index;

        var dropdownField = new DropdownField("Save id", allSaveIds, defaultIndex);

        dropdownField.RegisterValueChangedCallback(OnDebuggingSaveIdChanged);

        rootVisualElement.Add(dropdownField);
    }

    private void OnDebuggingSaveIdChanged(ChangeEvent<string> evt)
    {
        using var realm = ServiceLocator.GetSingleton<IRealmContext>().GetGlobalRealm();
        realm.WriteUpsert<DebuggingRealm>(debugginRealm =>
        {
            debugginRealm.SaveId = long.Parse(evt.newValue);
        });
    }
}