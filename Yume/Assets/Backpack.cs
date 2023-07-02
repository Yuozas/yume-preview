using UnityEngine;

public class Backpack : MonoBehaviour, IInteractable
{
    [Header("References")]
    [SerializeField] private InteractionScriptableObject _interaction;
    [SerializeField] private EventScriptableObject _event;

    private void Awake()
    {
        if (_event.Invoked)
            SelfDestroy();
    }

    private void Start()
    {
        _event.Event += SelfDestroy;
    }

    private void OnDestroy()
    {
        _event.Event -= SelfDestroy;
    }

    public bool CanInteract()
    {
        return true;
    }

    public void Interact()
    {
        _interaction.Interact();
    }

    private void SelfDestroy()
    {
        Destroy(gameObject);
    }
}
