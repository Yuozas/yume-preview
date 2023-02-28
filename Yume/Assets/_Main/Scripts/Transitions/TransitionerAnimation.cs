using UnityEngine;
using UnityEngine.UI;
using System;

public class TransitionerAnimation : Singleton<TransitionerAnimation>
{
    [Header("References")]
    [SerializeField] Image _image;

    [Header("Settings")]
    [SerializeField] Color _clear;
    [SerializeField] Color _default;

    public const float DURATION = 0.33f;

    Percentage _percentage;
    Color _from;
    Color _to;

    protected override void Awake()
    {
        base.Awake();
        _percentage = new Percentage(this);
        _percentage.OnUpdated += Set;
    }

    void OnDestroy() => _percentage.OnUpdated -= Set;

    public void ToClear()
    {
        _from = _default;
        _to = _clear;

        _percentage.Play(DURATION);
    }

    public void ToDefault(Action onCompleted = null)
    {
        _from = _clear;
        _to = _default;

        _percentage.Play(DURATION, onCompleted);
    }

    void Set(float percentage) => _image.color = Color.Lerp(_from, _to, percentage);
}
