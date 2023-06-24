using SwiftLocator.Services.ServiceLocatorServices;
using UnityEditor;
using UnityEngine;

public static class RealmUnityEditor
{
    [MenuItem("Tools/Realm/Delete global realm")]
    private static void DeleteGlobalRealm()
    {
        ServiceLocator.GetSingleton<IRealmContext>().DeleteGlobalRealm();
    }

    [MenuItem("Tools/Realm/Log global realm path")]
    private static void LogRealmPath()
    {
        var realm = ServiceLocator.GetSingleton<IRealmContext>().GetGlobalRealm();
        var path = realm.Config.DatabasePath;
        Debug.Log(path);
    }
}