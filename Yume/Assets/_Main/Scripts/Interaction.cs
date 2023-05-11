using UnityEngine;

public class Interaction
{
    public const float DEFAULT_INTERACTION_DISTANCE = 1f;

    public bool TryInteract(Vector2 position, Vector2 direction, float distance)
    {
        var hit = Physics2D.Raycast(position, direction, distance);

        if (hit.collider is null)
            return false;

        var found = hit.collider.TryGetComponent<Conversation>(out var conversation);
        if (!found)
            return false;

        conversation.Interact();
        return true;
    }
}

