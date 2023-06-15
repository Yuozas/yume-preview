using System;
using UnityEngine;

[Serializable]
public struct SfxClipSettings
{
    public static readonly SfxClipSettings Default = new(null);

    [SerializeReference] public AudioClip Clip;

    public SfxClipSettings(AudioClip clip)
    {
        Clip = clip;
    }
}