using UnityEngine;

public class Sfx : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private AudioSource _source;

    public void Play(SfxClipSettings settings)
    {
        _source.PlayOneShot(settings.Clip);
    }
}