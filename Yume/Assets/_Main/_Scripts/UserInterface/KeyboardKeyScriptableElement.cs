using UnityEngine;

[CreateAssetMenu(menuName = "General User Interface/Keyboard Key")]
public class KeyboardKeyScriptableElement : ScriptableObject
{
    [field: SerializeField] public Sprite Sprite { get; private set; }
    [field: SerializeField] public string ActionName { get; private set; }
}