using UnityEngine;
using System;
using System.Linq;

public class Music : MonoBehaviour
{
    [Header("References")]
    [SerializeField] AudioSource[] _sources;

    [Header("Settings")]
    [SerializeField] float _volume = 0.5f;

    public AudioSource Current => _sources[_index];
    public AudioSource Previous => _sources[1 - _index];

    Percentage _percentage;
    int _index;

    void Awake() => _percentage = new Percentage(this);
    void OnEnable() => _percentage.OnUpdated += Set;
    void OnDisable() => _percentage.OnUpdated -= Set;

    #if UNITY_EDITOR
    AudioSource[] _previous;
    #endif
    void OnValidate()
    {
        const int requiredSourcesLength = 2;
        if(_sources.Length > requiredSourcesLength)
            _sources = _sources.Take(requiredSourcesLength).ToArray();

        if (_previous?.Length == requiredSourcesLength && _sources.Length < requiredSourcesLength)
            _sources = _previous;

        _previous = _sources;
    }

    public void PlayInstant(AudioClip clip)
    {
        Current.Stop();

        UpdateActiveSourceIndex();
        SetClipAndPlay(clip);
    }

    public void Play(AudioClip clip, float duration)
    {
        UpdateActiveSourceIndex();
        SetClipAndPlay(clip);

        _percentage.Play(duration, StopPreviousSource);
    }

    void UpdateActiveSourceIndex() => _index = 1 - _index;
    void StopPreviousSource() => Previous.Stop();
    void SetClipAndPlay(AudioClip clip)
    {
        Current.clip = clip;
        Current.Play();
    }
    void Set(float percentage)
    {
        Current.volume = Mathf.Lerp(0f, _volume, percentage);
        Previous.volume = Mathf.Lerp(_volume, 0f, percentage);
    }
}
