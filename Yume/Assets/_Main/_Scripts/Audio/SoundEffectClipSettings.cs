using System;
using UnityEngine;

[Serializable]
public struct SoundEffectClipSettings
{
    public static readonly SoundEffectClipSettings Default = new(null);

    [SerializeReference] public AudioClip Clip;

    public SoundEffectClipSettings(AudioClip clip)
    {
        Clip = clip;
    }
}