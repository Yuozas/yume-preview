using System.IO;
using UnityEngine.SceneManagement;

public class SceneHelper : ISceneHelper
{
    public string[] GetAllSceneNames()
    {
        var sceneCount = SceneManager.sceneCountInBuildSettings;
        var sceneNames = new string[sceneCount];

        for (var i = 0; i < sceneCount; i++)
            sceneNames[i] = Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(i));

        return sceneNames;
    }
}