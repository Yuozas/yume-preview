using System.Collections.Generic;

public readonly struct ItemType
{
    public static readonly ItemType None = new(1, "Item");
    public static readonly ItemType Weapon = new(2, "Weapon");
    public static readonly ItemType Headwear = new(3, "Headwear");
    public static readonly ItemType Footwear = new(4, "Footwear");
    public static readonly ItemType TorsoClothes = new(5, "Torso clothes");
    public static readonly ItemType Legwear = new(6, "Legwear");
    public static readonly ItemType Gloves = new(7, "Gloves");
    public static readonly ItemType Ring = new(8, "Ring");
    public static readonly ItemType Necklace = new(9, "Necklace");
    public static readonly ItemType Earring = new(10, "Earring");

    public static Dictionary<int, ItemType> ItemTypes { get; } = ReflectionUtility.GetStaticFieldDictionary<ItemType>();

    private ItemType(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public int Id { get; }
    public string Name { get; }

    public static implicit operator ItemTypeRealmObject(ItemType itemType) => new() { Id = itemType.Id, Name = itemType.Name };
}