using SwiftLocator.Services.ServiceLocatorServices;
using UnityEngine;

public class SliderGameUserInterface : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject _visualization;
    [SerializeField] private RectTransform _slider;
    [SerializeField] private RectTransform _zone;
    [SerializeField] private RectTransform _green;

    private SliderGame _game;
    private IToggler _toggler;

    private void Awake()
    {
        _game = ServiceLocator.GetSingleton<SliderGame>();
        _toggler = _game.Toggler;
        Set(false);
    }

    private void OnEnable()
    {
        _toggler.OnUpdated += Set;
        _game.OnPercentageUpdated += Adjust;
    }

    private void OnDisable()
    {
        _toggler.OnUpdated -= Set;
        _game.OnPercentageUpdated -= Adjust;
    }

    private void Set(bool active)
    {
        _visualization.SetActive(active);
        if (active)
            Adjust();
    }

    private void Adjust()
    {
        var center = _zone.rect.center.y;
        var left = new Vector3(_zone.rect.xMin, center, 0f);
        var right = new Vector3(_zone.rect.xMax, center, 0f);

        var stage = _game.Current;

        var size = Mathf.Lerp(0f, Mathf.Abs(_zone.sizeDelta.x), stage.TargetBoxElementSize);

        _green.sizeDelta = new Vector2(size, _green.sizeDelta.y);
        _green.localPosition = Vector3.Lerp(left, right, stage.TargetBoxSpawnPosition);

        _slider.localPosition = Vector3.Lerp(left, right, _game.CursorPosition);
    }
}
