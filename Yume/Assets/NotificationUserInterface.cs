using UnityEngine;
using TMPro;
using System;

public class NotificationUserInterface : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TMP_Text _text;
    [SerializeField] private CanvasGroup _group;

    [Header("Settings")]
    [SerializeField] private float _duration = 3.25f;
    [SerializeField] private float _from = 1f;
    [SerializeField] private float _to = 0f;
    [SerializeField] private AnimationCurve _curve;

    public string Name { get; private set; }
    private Percentage _percentage;
    private Action<NotificationUserInterface> _action;

    public void Initialize(string name, Action<NotificationUserInterface> action)
    {
        Name = name;

        _action = action;
        _text.text = name;

        _percentage = new Percentage(this);
        _percentage.OnUpdated += Set;
        _percentage.Begin(_duration, SelfDestroy);
    }

    private void OnDestroy()
    {
        _percentage.OnUpdated -= Set;
    }

    private void Set(float percentage)
    {
        percentage = _curve.Evaluate(percentage);
        var value = Mathf.Lerp(_from, _to, percentage);
        _group.alpha = value;
    }

    private void SelfDestroy()
    {
        _action?.Invoke(this);
        Destroy(gameObject);
    }
}
