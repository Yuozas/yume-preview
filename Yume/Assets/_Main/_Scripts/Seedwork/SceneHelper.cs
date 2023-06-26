using System.IO;
using UnityEngine.SceneManagement;

public class SceneHelper : ISceneHelper
{
    private readonly IRealmSaveManager _realmSaveManager;

    public SceneHelper(IRealmSaveManager realmSaveManager)
    {
        _realmSaveManager = realmSaveManager;
    }

    public void LoadActiveSaveScene()
    {
        using var realm = _realmSaveManager.GetActiveSave();
        var playerDetails = realm.Get<ActiveCharacer>();
        SceneManager.LoadScene(playerDetails.Character.SceneName, LoadSceneMode.Single);
    }

    public string[] GetAllSceneNames()
    {
        var sceneCount = SceneManager.sceneCountInBuildSettings;
        var sceneNames = new string[sceneCount];

        for (var i = 0; i < sceneCount; i++)
            sceneNames[i] = Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(i));

        return sceneNames;
    }
}