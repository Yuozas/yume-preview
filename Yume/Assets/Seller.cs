using SwiftLocator.Services.ServiceLocatorServices;
using UnityEngine;

public class Seller : Entity, IInteractable
{
    [Header("References")]
    [SerializeField] private InteractionScriptableObject _interaction;
    [SerializeField] private StoreScriptableObject _store;

    public bool CanInteract()
    {
        return _interaction is not null;
    }

    public void Interact()
    {
        _interaction.Interact();
        var store = ServiceLocator.GetSingleton<IStoreManager>().GetCreateStore(_store);
        ServiceLocator.GetSingleton<StoreUserInterface>().Enter(store.Result.Id);
    }
}
