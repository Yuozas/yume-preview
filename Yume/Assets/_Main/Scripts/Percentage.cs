using System.Collections;
using UnityEngine;
using System;

public class Percentage
{
    public event Action<float> OnUpdated;

    MonoBehaviour _behaviour;
    IEnumerator _ienumerator;
    public Percentage(MonoBehaviour behaviour) => _behaviour = behaviour;

    void Stop()
    {
        if (_ienumerator == null) return;

        _behaviour.StopCoroutine(_ienumerator);
        _ienumerator = null;
    }

    public void Play(float duration, Action onCompleted = null)
    {
        Stop();

        _ienumerator = Co_Play(duration, onCompleted);
        _behaviour.StartCoroutine(_ienumerator);
    }
    IEnumerator Co_Play(float duration, Action onCompleted = null)
    {
        var percentage = 0f;
        while (percentage < 1f)
        {
            percentage += Time.deltaTime * 1f / duration;
            OnUpdated?.Invoke(percentage);
            yield return null;
        }
        
        onCompleted?.Invoke();
    }
}