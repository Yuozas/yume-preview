using SwiftLocator.Services.ServiceLocatorServices;
using System;
using UnityEngine;

[Serializable]
public class PlayMusicClipCommand : ICommand
{
    [SerializeField] private MusicClipSettings _settings;

    public PlayMusicClipCommand(MusicClipSettings settings)
    {
        _settings = settings;
    }

    public void Execute(Action onFinished = null)
    {
        ServiceLocator.GetSingleton<Music>().Play(_settings);
        onFinished?.Invoke();
    }
}