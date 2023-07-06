using SwiftLocator.Services.ServiceLocatorServices;
using UnityEngine;

public class Tadas : Entity, IInteractable
{
    [Header("References")]
    [SerializeField] private InteractionScriptableObject _interaction;
    [SerializeField] private EventScriptableObject _event;

    private void OnEnable()
    {
        _event.Event += Quit;
    }

    private void OnDisable()
    {
        _event.Event -= Quit;
    }

    private void Quit()
    {
        ServiceLocator.GetSingleton<IRealmSaveManager>().DeleteActiveSave();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public bool CanInteract()
    {
        return _interaction is not null;
    }

    public void Interact()
    {
        _interaction.Interact();
    }
}