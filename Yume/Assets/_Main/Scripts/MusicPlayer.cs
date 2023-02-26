using UnityEngine;
using UnityEngine.InputSystem;

public class MusicPlayer : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Music _music;
    [SerializeField] AudioClip[] _clips;

    [Header("Settings")]
    [SerializeField] Key _switch;

    public const float DEFAULT_FADE_DURATION = 1f;
    AudioClip CurrentClip => _clips[_index];
    int _index;

    void Start() => _music.PlayInstant(CurrentClip);

    void Update()
    {
        var keyboard = Keyboard.current;
        if (keyboard == null) return;
        if (!keyboard[_switch].wasPressedThisFrame) return;

        UpdateCurrentAudioClip();
        _music.Play(CurrentClip, DEFAULT_FADE_DURATION);
    }

    void UpdateCurrentAudioClip()
    {
        _index++;
        if (_index >= _clips.Length) _index = 0;
    }
}
