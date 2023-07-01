using UnityEngine;

public class SoundEffectAudioSource : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private AudioSource _source;

    public void Play(SoundEffectClipSettings settings)
    {
        _source.PlayOneShot(settings.Clip);
    }
}