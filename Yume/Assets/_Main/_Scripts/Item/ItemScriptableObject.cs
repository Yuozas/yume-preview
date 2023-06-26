using UnityEngine;

[CreateAssetMenu( menuName = "ScriptableObjects/Item")]
public class ItemScriptableObject : ScriptableObject
{
    [field: SerializeReference] public long Id { get; private set; }
    [field: SerializeReference] public string Name { get; private set; }
    [field: SerializeReference] public ItemType ItemType { get; private set; }
    [field: SerializeReference] public Sprite ItemIcon { get; private set; }

    public static implicit operator Item(ItemScriptableObject itemScriptableObject)
    {
        return new() 
        { 
            Id = itemScriptableObject.Id, 
            Name = itemScriptableObject.Name, 
            Type = itemScriptableObject.ItemType,
            ItemIcon = itemScriptableObject.ItemIcon.ToBytes()
        };
    }
}