using UnityEngine;

public class CampingHouse : MonoBehaviour, IInteractable
{
    [Header("References")]
    [SerializeField] private InteractionScriptableObject _needLockPick;
    [SerializeField] private InteractionScriptableObject _gameToUnlock;
    [SerializeField] private InteractionScriptableObject _enter;

    [SerializeField] private EventScriptableObject _lockPickFound;
    [SerializeField] private EventScriptableObject _doorUnlocked;

    public bool CanInteract()
    {
        return true;
    }

    public void Interact()
    {
        if (_doorUnlocked.Invoked)
            _enter.Interact();
        else if (_lockPickFound.Invoked)
            _gameToUnlock.Interact();
        else
            _needLockPick.Interact();
    }
}
