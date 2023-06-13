using System;
using UnityEngine;

[Serializable]
public struct MusicClipSettings
{
    public static readonly MusicClipSettings Default = new(null, 1);

    [SerializeReference] public AudioClip Clip;
    public float CrossFadeDuration;

    public MusicClipSettings(AudioClip clip, float duration)
    {
        Clip = clip;
        CrossFadeDuration = duration;
    }
}