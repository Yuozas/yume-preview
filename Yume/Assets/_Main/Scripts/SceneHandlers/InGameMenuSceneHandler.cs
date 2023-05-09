using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenuSceneHandler : MonoBehaviour
{
    bool _triggeredTransition;

    public void LoadInGameScene()
    {
        if (_triggeredTransition)
            return;
        _triggeredTransition = true;
        StartCoroutine(Co_WaitAllCoroutines());
    }

    void OnEnable() => _triggeredTransition = false;

    IEnumerator Co_WaitAllCoroutines()
    {
        yield return Co_WaitForInGameSceneToDisapear();
        yield return Co_LoadInGameScene();
    }

    IEnumerator Co_WaitForInGameSceneToDisapear()
    {
        var menuScene = SceneManager.GetSceneByName(SceneData.InGameSceneName);
        while (menuScene.isLoaded)
            yield return null;
    }

    IEnumerator Co_LoadInGameScene()
    {
        var loadTask = SceneManager.LoadSceneAsync(SceneData.InGameSceneName, LoadSceneMode.Additive);
        while (!loadTask.isDone)
            yield return null;
        var unloadTask = SceneManager.UnloadSceneAsync(SceneData.InGameMenuSceneName);
        while (!unloadTask.isDone)
            yield return null;
    }
}
