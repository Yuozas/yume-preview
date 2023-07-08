using SwiftLocator.Services.ServiceLocatorServices;
using UnityEngine;
using System.Linq;

public class Tadas : Entity, IInteractable
{
    [Header("References")]
    [SerializeField] private InteractionScriptableObject _interaction;
    [SerializeField] private InteractionScriptableObject _goodEndingInteraction;
    [SerializeField] private EventScriptableObject _event;
    [SerializeField] private ItemScriptableObject _item;

    private void OnEnable()
    {
        _event.Event += Quit;
    }

    private void OnDisable()
    {
        _event.Event -= Quit;
    }

    private void Quit()
    {
        ServiceLocator.GetSingleton<IRealmSaveManager>().DeleteActiveSave();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public bool CanInteract()
    {
        return _interaction is not null;
    }

    public void Interact()
    {
        using var backpack = ServiceLocator.GetSingleton<IActiveCharacterHelper>().GetBackpack();

        var containsApple = false;
        foreach (var slot in backpack.Result.StorageSlots)
        {
            if(slot.Item?.Id == _item.Id)
            {
                containsApple = true;
                break;
            }
        }

        if (containsApple)
            _goodEndingInteraction.Interact();
        else
            _interaction.Interact();
    }
}