using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/SceneInteraction")]
public class SceneInteractionScriptableObject : ScriptableObject
{
    [field: SerializeField] public InteractionScriptableObject Interaction { get; private set; }
    [field: SerializeField] public string SceneName { get; private set; }
}