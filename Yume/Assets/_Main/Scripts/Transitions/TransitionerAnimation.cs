using UnityEngine;
using UnityEngine.UI;
using System;

public class TransitionerAnimation : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Image _image;

    [Header("Settings")]
    [SerializeField] private Color _clear;
    [SerializeField] private Color _default;

    public const float DURATION = 0.33f;

    private Percentage _percentage;
    private Color _from;
    private Color _to;

    private void Awake()
    {
        _percentage = new Percentage(this);
        _percentage.OnUpdated += Set;
    }

    private void OnDestroy()
    {
        _percentage.OnUpdated -= Set;
    }

    public void ToClear()
    {
        _from = _default;
        _to = _clear;

        _percentage.Begin(DURATION);
    }

    public void ToDefault(Action onCompleted = null)
    {
        _from = _clear;
        _to = _default;

        _percentage.Begin(DURATION, onCompleted);
    }

    private void Set(float percentage)
    {
        _image.color = Color.Lerp(_from, _to, percentage);
    }
}