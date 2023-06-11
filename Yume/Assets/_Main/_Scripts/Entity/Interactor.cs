using UnityEngine;

public class Interactor
{
    public const float DEFAULT_INTERACTION_DISTANCE = 1f;
    private const float VERTICAL_OFFSET = 0.05f;

    public bool TryInteract(Vector2 position, Vector2 direction, float distance)
    {
        var hit = Physics2D.Raycast(position + (Vector2.up * VERTICAL_OFFSET), direction, distance);

        if (hit.collider is null)
            return false;

        var found = hit.collider.TryGetComponent<IInteractable>(out var interactable);
        if (!found)
            return false;

        interactable.Interact();
        return true;
    }
}

