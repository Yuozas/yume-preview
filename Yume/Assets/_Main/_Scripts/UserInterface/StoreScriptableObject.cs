using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Store/Store")]
public class StoreScriptableObject : ScriptableObject
{
    [field: SerializeField] public string Id { get; set; }
    [field: SerializeField] public string StoreName { get; private set; }
    [field: SerializeField] public List<StoreItemScripatableObject> StoreItems { get; private set; }
    [field: SerializeField] public Sprite Sprite { get; private set; }
    [field: SerializeField] public string IconPath { get; private set; }

#if UNITY_EDITOR
    public void OnValidate()
    {
        IconPath = Sprite.GetResourcesPath();
    }
#endif
}
