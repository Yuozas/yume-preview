using UnityEngine;

public class Npc : Entity, IInteractable
{
    [Header("References")]
    [SerializeField] private InteractionScriptableObject _interaction;
    [SerializeField] private InteractionScriptableObject _giveInteraction;
    [SerializeField] private InteractionScriptableObject _goAwayInteraction;

    [SerializeField] private EventScriptableObject _fixedPanelEvent;
    [SerializeField] private EventScriptableObject _obtainedLockPick;

    public bool CanInteract()
    {
        return _interaction is not null;
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
}