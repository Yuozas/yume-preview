using UnityEngine;

[CreateAssetMenu( menuName = "ScriptableObjects/Item")]
public class ItemScriptableObject : ScriptableObject
{
    [field: SerializeField] public string Id { get; private set; }
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public Sprite ItemIcon { get; private set; }
    [field: SerializeField] public string Description { get; private set; }
    [field: SerializeField] public string ItemIconPath { get; private set; }

    public void OnValidate()
    {
        if(ItemIcon == null)
            return;
        ItemIconPath = ItemIcon.GetResourcesPath();
    }

    public static implicit operator Item(ItemScriptableObject itemScriptableObject)
    {
        if(itemScriptableObject == null)
            return null;
        return new() 
        { 
            Id = itemScriptableObject.Id, 
            Name = itemScriptableObject.Name, 
            Description = itemScriptableObject.Description,
            IconPath = itemScriptableObject.ItemIconPath
        };
    }
}