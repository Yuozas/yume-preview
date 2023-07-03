using SwiftLocator.Services.ServiceLocatorServices;
using UnityEngine;

public class Npc : Entity, IInteractable
{
    [Header("References")]
    [SerializeField] private ItemScriptableObject _item;
    [SerializeField] private InteractionScriptableObject _interaction;
    [SerializeField] private InteractionScriptableObject _giveInteraction;
    [SerializeField] private InteractionScriptableObject _goAwayInteraction;

    [SerializeField] private EventScriptableObject _fixedPanelEvent;
    [SerializeField] private EventScriptableObject _obtainedLockPick;

    public bool CanInteract()
    {
        return _interaction is not null;
    }

    private void OnEnable()
    {
        _obtainedLockPick.Event += AddItemToBackpack;
    }

    private void OnDisable()
    {

        _obtainedLockPick.Event -= AddItemToBackpack;
    }

    public void Interact()
    {
        if (_obtainedLockPick.Invoked)
            _goAwayInteraction.Interact();
        else if (_fixedPanelEvent.Invoked)
            _giveInteraction.Interact();
        else
            _interaction.Interact();
    }

    private void AddItemToBackpack()
    {
        if (_item == null)
            return;
        ServiceLocator.GetSingleton<IStorageItemHelper>().TryAddItemToBackpack(_item);
    }
}