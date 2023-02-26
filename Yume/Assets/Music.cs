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

    Percentage _percentage;
    int _index;

    void Awake() => _percentage = new Percentage(this);
    void OnEnable() => _percentage.OnUpdated += Set;
    void OnDisable() => _percentage.OnUpdated -= Set;

    #if UNITY_EDITOR
    AudioSource[] _previous;
    void OnValidate()
    {
        const int REQUIRED_SOURCES_LENGTH = 2;
        if(_sources.Length > REQUIRED_SOURCES_LENGTH) _sources = _sources.Take(REQUIRED_SOURCES_LENGTH).ToArray();
        if (_previous != null && _previous.Length == REQUIRED_SOURCES_LENGTH && _sources.Length < REQUIRED_SOURCES_LENGTH) _sources = _previous;

        _previous = new AudioSource[_sources.Length];
        Array.Copy(_sources, _previous, _sources.Length);
    }
    #endif
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
    void StopPreviousSource()
    {
        var previous = _sources[1 - _index];
        previous.Stop();
    }
    void SetClipAndPlay(AudioClip clip)
    {
        Current.clip = clip;
        Current.Play();
    }

    void Set(float percentage)
    {
	    _sources[_index].volume = Mathf.Lerp(0f, _volume, percentage);
	    _sources[1 - _index].volume = Mathf.Lerp(_volume, 0f, percentage);
    }
}
