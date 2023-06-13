using UnityEngine;

public class Interactor : IInteractor
{
    public bool TryInteract(Vector2 position, Vector2 direction, float distance)
    {
        var hit = Physics2D.Raycast(position + (Vector2.up * IInteractor.VERTICAL_OFFSET), direction, distance);

        var found = hit.collider.TryGetComponent<IInteractable>(out var interactable);
        if (!found)
            return false;

        var can = interactable.Can();
        if (!can)
            return false;

        interactable.Interact();
        return true;
    }
}