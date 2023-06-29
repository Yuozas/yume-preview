using UnityEngine;
using System.Linq;

public class Music : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private AudioSource[] _sources;

    [Header("Settings")]
    [SerializeField] private float _volume = 0.5f;

    public AudioSource CurrentSource => _sources[_index];
    public AudioSource PreviousSource => _sources[1 - _index];

    private Percentage _percentage;
    private int _index;

    private void Awake()
    {
        _percentage = new Percentage(this);
        _percentage.OnUpdated += Set;
    }

    private void OnDestroy()
    {
        _percentage.OnUpdated -= Set;
    }

#if UNITY_EDITOR
    private AudioSource[] _previousSources;

    private void OnValidate()
    {
        const int requiredSourcesLength = 2;
        if (_sources.Length is requiredSourcesLength)
            _previousSources = _sources.ToArray();
        else
            _sources = _previousSources?.ToArray();
    }
#endif
    public void PlayInstant(AudioClip clip)
    {
        if (CurrentSource.clip == clip)
            return;

        CurrentSource.Stop();

        UpdateActiveSourceIndex();
        SetClipAndPlay(clip);
    }

    public void Play(AudioClip clip, float duration)
    {
        if (CurrentSource.clip == clip)
            return;

        UpdateActiveSourceIndex();
        SetClipAndPlay(clip);

        _percentage.Begin(duration, StopPreviousSource);
    }

    public void Play(MusicClipSettings settings)
    {
        if (CurrentSource.clip == settings.Clip)
            return;

        if(settings.CrossFadeDuration <= Mathf.Epsilon)
        {
            PlayInstant(settings.Clip);
            return;
        }

        Play(settings.Clip, settings.CrossFadeDuration);
    }

    private void UpdateActiveSourceIndex()
    {
        _index = 1 - _index;
    }

    private void StopPreviousSource()
    {
        PreviousSource.Stop();
    }

    private void SetClipAndPlay(AudioClip clip)
    {
        CurrentSource.clip = clip;
        CurrentSource.Play();
    }

    private void Set(float percentage)
    {
        CurrentSource.volume = Mathf.Lerp(0, _volume, percentage);
        PreviousSource.volume = Mathf.Lerp(_volume, 0, percentage);
    }
}
