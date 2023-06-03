using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionTesting : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Interaction _interaction;

    [ContextMenu("Add")]
    public void Add()
    {
        _interaction.Set();
    }
}
