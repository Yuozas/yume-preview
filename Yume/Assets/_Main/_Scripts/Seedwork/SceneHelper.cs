using System.IO;
using UnityEngine.SceneManagement;

public class SceneHelper : ISceneHelper
{
    private readonly IRealmActiveSaveHelper _realmActiveSaveHelper;

    public SceneHelper(IRealmActiveSaveHelper realmActiveSaveHelper)
    {
        _realmActiveSaveHelper = realmActiveSaveHelper;
    }

    public void LoadActiveSaveScene()
    {
        using var realm = _realmActiveSaveHelper.GetActiveSave();
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