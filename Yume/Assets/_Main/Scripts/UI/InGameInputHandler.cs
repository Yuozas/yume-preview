using UnityEngine;
using UnityEngine.InputSystem;

public class InGameInputHandler : MonoBehaviour
{
    [SerializeField] InGameSceneHandler _inGameSceneHandler;
    InputActions _inputActions;

    private void OnEnable()
    {
        _inputActions = new();
        _inputActions.Enable();
        _inputActions.Ingame.Backpackmenu.performed += LoadBackpackMenu;
        _inputActions.Ingame.Settingsmenu.performed += LoadSettingsMenu;
    }

    void LoadBackpackMenu(InputAction.CallbackContext _) => _inGameSceneHandler.LoadInGameMenu(InGameMenuOption.Backpack);
    void LoadSettingsMenu(InputAction.CallbackContext _) => _inGameSceneHandler.LoadInGameMenu(InGameMenuOption.Settings);
}
