using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Storage")]
public class StorageScriptableObject : ScriptableObject
{
    // Todo. Refactor public setters.
    [field: SerializeField] public string Id { get; set; }
    [field: SerializeField] public string Name { get; set; }
    [field: SerializeField] public int Slots { get; set; }

    public static implicit operator Storage(StorageScriptableObject storageScriptableObject)
    {
        return new Storage
        {
            Id = storageScriptableObject.Id,
            Name = storageScriptableObject.Name
        };
    }
}