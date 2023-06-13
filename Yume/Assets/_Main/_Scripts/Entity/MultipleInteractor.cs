﻿using UnityEngine;
using System.Linq;

public class MultipleInteractor : IInteractor
{
    public bool TryInteract(Vector2 position, Vector2 direction, float distance)
    {
        var hits = Physics2D.RaycastAll(position + (Vector2.up * IInteractor.VERTICAL_OFFSET), direction, distance);
        var ordered = hits
            .OrderBy(hit => hit.distance)
            .Select(hit => hit.collider.GetComponent<IInteractable>())
            .Where(interactable => interactable is not null && interactable.CanInteract())
            .FirstOrDefault();

        if(ordered is null)
            return false;

        ordered.Interact();
        return true;
    }
}