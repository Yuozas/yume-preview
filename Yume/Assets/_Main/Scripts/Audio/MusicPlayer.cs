using UnityEngine;
using UnityEngine.InputSystem;

public class MusicPlayer : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Music _music;
    [SerializeField] private AudioClip[] _clips;

    [Header("Settings")]
    [SerializeField] private Key _switch;

    public const float DEFAULT_FADE_DURATION = 1;

    private AudioClip CurrentClip => _clips[_index];
    private int _index;

    private void Start()
    {
        _music.PlayInstant(CurrentClip);
    }

    private void Update()
    {
        var keyboard = Keyboard.current;
        if (keyboard is null)
            return;
        if (!keyboard[_switch].wasPressedThisFrame)
            return;

        UpdateCurrentAudioClip();
        _music.Play(CurrentClip, DEFAULT_FADE_DURATION);
    }

    private void UpdateCurrentAudioClip()
    {
        _index++;
        if (_index >= _clips.Length)
            _index = 0;
    }
}
