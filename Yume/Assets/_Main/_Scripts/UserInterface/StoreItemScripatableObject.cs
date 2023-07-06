using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Store/Item")]
public class StoreItemScripatableObject : ScriptableObject
{
    [field: SerializeField] public ItemScriptableObject Item { get; private set; }
    [field: SerializeField] public float Price { get; private set; }
}