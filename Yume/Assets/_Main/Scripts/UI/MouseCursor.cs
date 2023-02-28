using UnityEngine;

public class MouseCursor : MonoBehaviour
{
    [SerializeField] Sprite _defaultCursor;
    SpriteRenderer _spriteRenderer;

    void OnDisable() => Cursor.visible = true;

    void OnEnable()
    {
        HideCursor();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        SetDefaultCursor();
    }

    void OnApplicationFocus(bool focus)
    {
        if (focus)
            HideCursor();
    }

    void Update()
    {
        var mousePositionByCamera = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(mousePositionByCamera.x, mousePositionByCamera.y, transform.position.z);
    }

    public void SetDefaultCursor() => _spriteRenderer.sprite = _defaultCursor;

    void HideCursor() => Cursor.visible = false;
}
