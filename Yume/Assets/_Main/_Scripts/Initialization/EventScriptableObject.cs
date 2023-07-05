using System;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Event")]
public class EventScriptableObject : ScriptableObject
{
    public event Action Event;
    [NonSerialized] public bool Invoked;

    public void InvokeEvent()
    {
        Event?.Invoke();
        Invoked = true;
    }
}