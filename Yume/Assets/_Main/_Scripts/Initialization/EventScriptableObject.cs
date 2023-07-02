using System;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Event")]
public class EventScriptableObject : ScriptableObject
{
    public event Action Event;
    public bool Invoked;

    public void Invoke()
    {
        Event?.Invoke();
        Invoked = true;
    }
}