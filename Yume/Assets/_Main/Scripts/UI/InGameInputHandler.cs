using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class InGameInputHandler : MonoBehaviour
{
    InputActions _inputActions;
    bool _triggeredTransition;

    private void OnEnable()
    {
        _triggeredTransition = false;
        _inputActions = new();
        _inputActions.Enable();
        _inputActions.Ingame.Backpackmenu.performed += LoadBackpackMenu;
    }

    void LoadBackpackMenu(InputAction.CallbackContext _) => StartCoroutineLocal(Co_LoadBackpackMenu);

    void StartCoroutineLocal(Func<IEnumerator> coroutine)
    {
        if (_triggeredTransition)
            return;
        _triggeredTransition = true;
        StartCoroutine(coroutine());
    }

    IEnumerator Co_LoadBackpackMenu()
    {
        var loadTask = SceneManager.LoadSceneAsync(SceneData.InGameMenuSceneName, LoadSceneMode.Additive);
        while (!loadTask.isDone)
            yield return null;
        var unloadTask = SceneManager.UnloadSceneAsync(SceneData.InGameSceneName);
        while(!unloadTask.isDone)
            yield return null;
    }
}
