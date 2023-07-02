using System;
using UnityEngine;

public class ElectricalPanel : MonoBehaviour, IInteractable
{
    [Header("References")]
    [SerializeField] private InteractionScriptableObject _interaction;
    [SerializeField] private InteractionScriptableObject _fixedInteraction;
    [SerializeField] private EventScriptableObject _event;
    [SerializeField] private WindTurbine _turbine;
    [SerializeField] private ParticleSystem _particle;

    private void OnEnable()
    {
        _event.Event += Execute;
    }

    private void OnDisable()
    {
        _event.Event += Execute;
    }

    private void Execute()
    {
        _turbine.Set(true);
        _particle.Stop();
    }

    public bool CanInteract()
    {
        return _interaction is not null && _fixedInteraction is not null;
    }

    public void Interact()
    {
        if (_turbine.Activated)
        {
            _fixedInteraction.Interact();
            return;
        }

        _interaction.Interact();
    }
}
