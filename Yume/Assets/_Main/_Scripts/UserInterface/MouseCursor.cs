using UnityEngine;
using UnityEngine.UIElements;

public class MouseCursor : MonoBehaviour
{
    private VisualElement _cursor;

    private void OnDisable()
    {
        UnityEngine.Cursor.visible = true;
    }

    private void OnEnable()
    {
        HideCursor();
        var root = GetComponent<UIDocument>().rootVisualElement;
        _cursor = root.Q<VisualElement>("Body");
        _cursor.pickingMode = PickingMode.Ignore;
        _cursor.Q<VisualElement>("Cursor").pickingMode = PickingMode.Ignore;
    }

    private void OnApplicationFocus(bool focus)
    {
        if (focus)
            HideCursor();
    }

    private void Update()
    {
        var mousePosition = Input.mousePosition;
        mousePosition.y = Screen.height - mousePosition.y;
        _cursor.style.left = mousePosition.x;
        _cursor.style.top = mousePosition.y;
    }

    private void HideCursor()
    {
        UnityEngine.Cursor.visible = false;
    }
}
