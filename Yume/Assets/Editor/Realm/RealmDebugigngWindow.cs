using SwiftLocator.Services.ServiceLocatorServices;
using System.Linq;
using UnityEditor;
using UnityEngine.SceneManagement;
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
        var numberField = new IntegerField
        {
            label = "Save id"
        };

        using var realm = ServiceLocator.GetSingleton<IRealmContext>().GetGlobalRealm();
        if (realm.TryGet<DebuggingRealm>(out var debuggingRealm))
            numberField.value = debuggingRealm.SaveId;

        numberField.RegisterValueChangedCallback(OnDebuggingSaveIdChanged);

        rootVisualElement.Add(numberField);
    }

    private void OnDebuggingSaveIdChanged(ChangeEvent<int> evt)
    {
        using var realm = ServiceLocator.GetSingleton<IRealmContext>().GetGlobalRealm();
        if (realm.TryGet<DebuggingRealm>(out var debuggingRealm))
            realm.Write(() =>
            {
                debuggingRealm.SaveId = evt.newValue;
                realm.Add(debuggingRealm, true);
            });
        else
            realm.WriteAdd(new DebuggingRealm { SaveId = evt.newValue });
    }
}