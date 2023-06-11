using UnityEngine;

public class Decoration : MonoBehaviour, IInteractable
{
    [Header("References")]
    [SerializeField] private Interaction _interaction;

    public void Interact()
    {
        if(_interaction != null)
            _interaction.Interact();
    }
}
