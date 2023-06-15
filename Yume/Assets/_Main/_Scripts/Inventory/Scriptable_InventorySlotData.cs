using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Inventory/Slot", order = 1)]
public class Scriptable_InventorySlotData : ScriptableObject
{
    [field: SerializeField] public int TotalSpace { get; set; }
    [field: SerializeField] public string Name { get; set; }
    [field: SerializeField] public List<Scriptable_InventorySlotData> Slots { get; set; }
}
