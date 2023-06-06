using UnityEngine;
using System;
using System.Linq;

public class Music : Singleton<Music>
{
    [Header("References")]
    [SerializeField] AudioSource[] _sources;

    [Header("Settings")]
    [SerializeField] float _volume = 0.5f;

    public AudioSource CurrentSource => _sources[_index];
    public AudioSource PreviousSource => _sources[1 - _index];

    Percentage _percentage;
    int _index;

    protected override void Awake()
    {
        base.Awake();
        _percentage = new Percentage(this);
        _percentage.OnUpdated += Set;
    }

    void OnDestroy() => _percentage.OnUpdated -= Set;

#if UNITY_EDITOR
    AudioSource[] _previousSources;

    void OnValidate()
    {
        const int requiredSourcesLength = 2;
        if (_sources.Length is requiredSourcesLength) _previousSources = _sources.ToArray();
        else _sources = _previousSources?.ToArray();
    }
#endif
    public void PlayInstant(AudioClip clip)
    {
        CurrentSource.Stop();

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
    void StopPreviousSource() => PreviousSource.Stop();

    void SetClipAndPlay(AudioClip clip)
    {
        CurrentSource.clip = clip;
        CurrentSource.Play();
    }

    void Set(float percentage)
    {
        CurrentSource.volume = Mathf.Lerp(0, _volume, percentage);
        PreviousSource.volume = Mathf.Lerp(_volume, 0, percentage);
    }
}
