using System.Collections;
using UnityEngine;
using System;

public class LoopPercentage : CoroutineHandler
{
    public event Action<float> OnUpdated;

    private float _duration;
    private bool _increase;

    public LoopPercentage(MonoBehaviour behaviour = null) : base(behaviour) { }

    public void Begin(float duration)
    {
        _duration = duration;
        Begin();
    }

    protected override IEnumerator Execute(Action onFinished = null)
    {
        _increase = true;
        var percentage = 0f;
        while (true)
        {
            percentage = _increase ? Increase(percentage) : Decrease(percentage);
            OnUpdated?.Invoke(percentage);

            yield return null;
        }
    }

    private float Increase(float percentage)
    {
        percentage += Time.deltaTime * 1 / _duration;
        if(percentage >= 1)
        {
            _increase = false;
            percentage -= percentage - 1f;
        }

        return percentage;
    }

    private float Decrease(float percentage)
    {
        percentage -= Time.deltaTime * 1 / _duration;
        if (percentage <= 0)
        {
            _increase = true;
            percentage += 0f - percentage;
        }

        return percentage;
    }
}