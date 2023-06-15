using SwiftLocator.Services.ServiceLocatorServices;
using System;
using UnityEngine;

[Serializable]
public class PlayMusicClipCommand : ICommand
{
    [SerializeField] public MusicClipSettings Settings;

    public PlayMusicClipCommand(MusicClipSettings? settings = null)
    {
        Settings = settings ?? MusicClipSettings.Default;
    }

    public void Execute(Action onFinished = null)
    {
        ServiceLocator.GetSingleton<Music>().Play(Settings);
        onFinished?.Invoke();
    }
}