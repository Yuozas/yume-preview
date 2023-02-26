using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Inventory/Visuals", order = 1)]
public class Scriptable_InventoryVisualData : ScriptableObject
{
    [field: SerializeField] public Sprite Sprite { get; set; }
}