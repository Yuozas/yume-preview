using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Inventory", order = 1)]
public class Scriptable_Inventory : ScriptableObject
{
    [field: SerializeField] public Scriptable_InventoryVisualData VisualData { get; set; }
    [field: SerializeField] public Scriptable_InventorySlotData SlotData { get; set; }
}
