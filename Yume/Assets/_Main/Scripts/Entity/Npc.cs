using UnityEngine;

public class Npc : Entity, IInteractable
{
    [Header("References")]
    [SerializeField] private Interaction _interaction;

    public void Interact()
    {
        _interaction.Interact();
    }
}