#if UNITY_EDITOR
using UnityEngine;

public class UnityEditorCursorDisable : MonoBehaviour
{
    InputActions _inputActions;

    void OnEnable()
    {
        _inputActions = new();
        _inputActions.Enable();
        _inputActions.UnityEditor.Escape.performed += HideCursor;
    }

    private void HideCursor(UnityEngine.InputSystem.InputAction.CallbackContext _) => Cursor.visible = false;
}
#endif