using System;
using UnityEngine;

[Serializable]
public struct MusicClipSettings
{
    [SerializeReference] public AudioClip Clip;
    public float CrossFadeDuration;

    public MusicClipSettings(AudioClip clip, float duration)
    {
        Clip = clip;
        CrossFadeDuration = duration;
    }
}
