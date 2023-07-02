using UnityEngine;
using UnityEngine.UI;
using System;

public class TransitionerAnimation : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private CanvasGroup _group;

    public const float DURATION = 0.5f;

    private Percentage _percentage;
    private float _from;
    private float _to;

    private void Awake()
    {
        _percentage = new Percentage(this);
        _percentage.OnUpdated += Set;

        _group.alpha = 0f;
    }

    private void OnDestroy()
    {
        _percentage.OnUpdated -= Set;
    }

    public void ToClear()
    {
        _from = 1f;
        _to = 0f;

        _percentage.Begin(DURATION);
    }

    public void ToDefault(Action onCompleted = null)
    {
        _from = 0f;
        _to = 1f;

        _percentage.Begin(DURATION, onCompleted);
    }

    private void Set(float percentage)
    {
        _group.alpha = Mathf.Lerp(_from, _to, percentage);
    }
}