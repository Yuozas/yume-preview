using SwiftLocator.Services.ServiceLocatorServices;
using UnityEngine;

public class Seller : Entity, IInteractable
{
    [Header("References")]
    [SerializeField] private InteractionScriptableObject _interaction;

    public bool CanInteract()
    {
        return _interaction is not null;
    }

    public void Interact()
    {
        _interaction.Interact();
    }
}
