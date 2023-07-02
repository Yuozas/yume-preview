using System;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Event")]
public class EventScriptableObject : ScriptableObject
{
    public event Action Event;
    public void Invoke()
    {
        Event?.Invoke();
    }
}