using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class InGameSceneHandler : MonoBehaviour
{
    bool _triggeredTransition;

    private void OnEnable() => _triggeredTransition = false;

    public void LoadInGameMenu(InGameMenuOption option)
    {
        if (_triggeredTransition)
            return;
        _triggeredTransition = true;
        StartCoroutine(Co_WaitAllCoroutines(option));
    }

    IEnumerator Co_WaitAllCoroutines(InGameMenuOption option)
    {
        yield return Co_WaitForMenuToDisapear();
        yield return Co_LoadInGameMenu(option);
    }

    IEnumerator Co_WaitForMenuToDisapear()
    {
        var menuScene = SceneManager.GetSceneByName(SceneData.InGameMenuSceneName);
        while (menuScene.isLoaded)
            yield return null;
    }

    IEnumerator Co_LoadInGameMenu(InGameMenuOption option)
    {
        var loadTask = SceneManager.LoadSceneAsync(SceneData.InGameMenuSceneName, LoadSceneMode.Additive);
        while (!loadTask.isDone)
            yield return null;

        SetInGameMenuOption(option);

        var unloadTask = SceneManager.UnloadSceneAsync(SceneData.InGameSceneName);
        while (!unloadTask.isDone)
            yield return null;
    }

    void SetInGameMenuOption(InGameMenuOption option)
    {
        var menuScene = SceneManager.GetSceneByName(SceneData.InGameMenuSceneName);
        var menuRoot = menuScene.GetRootGameObjects()[0];
        var optionHolder = menuRoot.AddComponent<InGameMenuOptionHolder>();
        optionHolder.Option = option;
    }
}
