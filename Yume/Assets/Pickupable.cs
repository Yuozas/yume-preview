using SwiftLocator.Services.ServiceLocatorServices;
using UnityEngine;

public class Pickupable : MonoBehaviour, IInteractable
{
    [Header("References")]
    [SerializeField] private InteractionScriptableObject _interaction;
    [SerializeField] private EventScriptableObject _event;
    [SerializeField] private ItemScriptableObject _item;

    private void Awake()
    {
        if (_event.Invoked)
            SelfDestroy();
    }

    private void Start()
    {
        _event.Event += AddToInventoryAndSelfDestroy;
    }

    private void OnDestroy()
    {
        _event.Event -= AddToInventoryAndSelfDestroy;
    }

    public bool CanInteract()
    {
        return true;
    }

    public void Interact()
    {
        _interaction.Interact();
    }

    private void AddToInventoryAndSelfDestroy()
    {
        // Todo. Should notify user if backpack is full. Otherwise throw.
        var ItemAddedSuccessfully = _item != null && ServiceLocator.SingletonProvider.Get<IStorageItemHelper>().TryAddItemToBackpack(_item);
        if (!ItemAddedSuccessfully)
            Debug.LogWarning($"Failed to add {_item.name} to backpack");
        else
            ServiceLocator.GetSingleton<Notifications>().Add(Notifications.INVENTORY_UPDATED);

        SelfDestroy();
    }

    private void SelfDestroy()
    {
        Destroy(gameObject);
    }
}
