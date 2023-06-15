using SwiftLocator.Services.ServiceLocatorServices;
using UnityEngine;

public class OnStartMusicPlayer : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private AudioClip _clip;

    private void Start()
    {
        ServiceLocator.GetSingleton<Music>().PlayInstant(_clip);
    }
}