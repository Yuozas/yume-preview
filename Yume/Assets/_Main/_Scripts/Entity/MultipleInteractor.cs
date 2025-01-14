﻿using UnityEngine;
using System.Linq;

public class MultipleInteractor : IInteractor
{
    public bool TryInteract(Vector2 position, Vector2 direction, float distance)
    {
        var hits = Physics2D.RaycastAll(position + (Vector2.up * IInteractor.VERTICAL_OFFSET), direction, distance);
        var closestInteractable = hits
            .OrderBy(hit => hit.distance)
            .SelectFirstValid(hit =>
                hit.collider.GetComponent<IInteractable>() is IInteractable interactable && interactable.CanInteract()
                    ? interactable
                    : null
            );

        if (closestInteractable is null)
            return false;

        closestInteractable.Interact();
        return true;
    }
}