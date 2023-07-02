using Realms;

public class  StorageSlotHasPrice : RealmObject
{
    public StorageSlot StorageSlot { get; set; }
    public float Price { get; set; }
}