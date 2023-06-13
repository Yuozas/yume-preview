using UnityEngine;

public class Decoration : MonoBehaviour, IInteractable
{
    [Header("References")]
    [SerializeField] private Interaction _interaction;

    public bool Can()
    {
        return _interaction is not null;
    }

    public void Interact()
    {
        _interaction.Interact();
    }
}
