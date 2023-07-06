using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Store/Store")]
public class StoreScriptableObject : ScriptableObject
{
    [field: SerializeField] public string Id { get; set; }
    [field: SerializeField] public string StoreName { get; private set; }
    [field: SerializeField] public List<StoreItemScripatableObject> StoreItems { get; private set; }
}
