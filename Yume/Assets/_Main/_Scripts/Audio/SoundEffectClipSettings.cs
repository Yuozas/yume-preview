using System;
using UnityEngine;

[Serializable]
public struct SoundEffectClipSettings
{
    public static readonly SoundEffectClipSettings Default = new(null, 1);

    [SerializeReference] public AudioClip Clip;
    [SerializeField] public float VolumeScale;

    public SoundEffectClipSettings(AudioClip clip, float volumeScale = 1)
    {
        Clip = clip;
        VolumeScale = volumeScale;
    }
}