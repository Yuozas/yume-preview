using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Storage")]
public class StorageScriptableObject : ScriptableObject
{
    public static StorageScriptableObject DemoBackpack { get; }

    static StorageScriptableObject()
    {
        DemoBackpack = CreateInstance<StorageScriptableObject>();
        DemoBackpack.Id = "1";
        DemoBackpack.Name = "Demo Backpack";
        DemoBackpack.Slots = 10;
    }

    [field: SerializeField] public string Id { get; private set; }
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public int Slots { get; private set; }

    public static implicit operator Storage(StorageScriptableObject storageScriptableObject)
    {
        return new Storage
        {
            Id = storageScriptableObject.Id,
            Name = storageScriptableObject.Name
        };
    }
}