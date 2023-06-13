using UnityEngine;

public interface IInteractor
{
    public const float DEFAULT_DISTANCE = 1.1f;
    public const float VERTICAL_OFFSET = 0.05f;

    bool TryInteract(Vector2 position, Vector2 direction, float distance);
}