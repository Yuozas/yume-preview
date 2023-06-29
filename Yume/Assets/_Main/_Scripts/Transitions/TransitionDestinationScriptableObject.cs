using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/TransitionDestination")]
public class TransitionDestinationScriptableObject : ScriptableObject
{
    [field: Header("References")]
    [field: SerializeField] public string SceneName { get; private set; } = "Scene";
}