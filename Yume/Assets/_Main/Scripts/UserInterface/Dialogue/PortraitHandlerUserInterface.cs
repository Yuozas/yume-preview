using UnityEngine;
using UnityEngine.UI;

public class PortraitHandlerUserInterface : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Image _face;
    [SerializeField] private Image _hair;

    private PortraitHandler _portrait;

    public void Initialize(PortraitHandler handler)
    {
        _portrait = handler;
        _portrait.OnUpdated += Set;

        Set(_portrait.Settings);
    }

    private void OnDestroy()
    {
        _portrait.OnUpdated -= Set;
    }

    private void Set(PortraitHandlerSettings settings)
    {
        Set(_face, settings.Face);
        Set(_hair, settings.Face);
    }

    private void Set(Image image, Sprite sprite)
    {
        image.sprite = sprite;
        image.color = sprite == null ? Color.clear : Color.white;
    }
}
