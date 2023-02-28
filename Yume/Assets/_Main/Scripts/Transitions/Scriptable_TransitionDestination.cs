using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable_TransitionDestination")]
public class Scriptable_TransitionDestination : ScriptableObject
{
    [field: Header("References")]
    [field: SerializeField] public string Scene { get; private set; } = "Scene";
}