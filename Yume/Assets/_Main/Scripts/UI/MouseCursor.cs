using UnityEngine;

public class MouseCursor : MonoBehaviour
{
    [SerializeField] Sprite _defaultCursor;
    SpriteRenderer _spriteRenderer;

    void OnEnable()
    {
        Cursor.visible = false;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        SetDefaultCursor();
    }

    void OnDisable() => Cursor.visible = true;
    void Update()
    {
        var mousePositionByCamera = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(mousePositionByCamera.x, mousePositionByCamera.y, transform.position.z);
    }
    public void SetDefaultCursor() => _spriteRenderer.sprite = _defaultCursor;
}
