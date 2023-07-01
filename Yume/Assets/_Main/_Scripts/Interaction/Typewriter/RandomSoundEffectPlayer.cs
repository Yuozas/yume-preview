using SwiftLocator.Services.ServiceLocatorServices;
using UnityEngine;

public class RandomSoundEffectPlayer : ISoundEffectPlayer
{
    private readonly SoundEffectAudioSource _sfx;
    private readonly AudioClip[] _clips;

    private readonly int _every;
    private int _current;

    public RandomSoundEffectPlayer(AudioClip[] clips, int every = 1)
    {
        _every = every;
        _clips = clips;
        _sfx = ServiceLocator.GetSingleton<SoundEffectAudioSource>();
    }

    public void Play()
    {
        _current++;
        if (_current < _every)
            return;

        var clip = _clips.GetRandom();
        var settings = new SoundEffectClipSettings(clip);
        _sfx.Play(settings);

        _current = 0;
    }
}