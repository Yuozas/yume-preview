using System.Collections;
using UnityEngine;
using System;

public class Percentage : CoroutineHandler
{
    public event Action<float> OnUpdated;

    private float _duration;

    public Percentage(MonoBehaviour behaviour) : base(behaviour) { }

    public void Begin(float duration, Action onFinished = null)
    {
        _duration = duration;
        Begin(onFinished);
    }

    protected override IEnumerator Execute(Action onFinished = null)
    {
        var percentage = 0f;
        while (percentage < 1)
        {
            percentage += Time.deltaTime * 1 / _duration;
            OnUpdated?.Invoke(percentage);
            yield return null;
        }

        onFinished?.Invoke();
    }
}