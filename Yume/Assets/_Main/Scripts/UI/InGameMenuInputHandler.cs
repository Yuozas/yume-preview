using UnityEngine;
using UnityEngine.InputSystem;

public class InGameMenuInputHandler : MonoBehaviour
{
    [SerializeField] InGameMenuSceneHandler _inGameMenuSceneHandler;
    InputActions _inputActions;

    private void OnEnable()
    {
        _inputActions = new();
        _inputActions.Enable();
        _inputActions.IngameMenu.Exit.performed += LoadInGameScene;
    }

    void LoadInGameScene(InputAction.CallbackContext _) => _inGameMenuSceneHandler.LoadInGameScene();
}
