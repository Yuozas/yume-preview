using UnityEngine;
using UnityEngine.UI;

public class PortraitHandlerUserInterface : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Image _image;

    private PortraitHandler _portrait;
    public void Initialize(PortraitHandler handler)
    {
        _portrait = handler;
        _portrait.OnUpdated += Set;

        Set(_portrait.Sprite);
    }

    private void OnDestroy()
    {
        _portrait.OnUpdated -= Set;
    }

    private void Set(Sprite sprite)
    {
        _image.sprite = sprite;
    }
}
