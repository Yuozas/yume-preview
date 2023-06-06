using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;
using Newtonsoft.Json;

public class InventoryHandler : MonoBehaviour
{
    [SerializeField] TextAsset _itemSaveFile;
    List<ItemInventorySave> _items;
    [SerializeField] Scriptable_Inventory _inventoryScriptable;

    void Awake() => ReadSave();
    public ItemInventorySave[] GetItems() => _items.ToArray();
    public void Add(ItemInventorySave itemInventorySave) => _items.Add(itemInventorySave);

    public void Remove(IEnumerable<string> pathFrom, int pathIndex = 0)
    {
        var path = ItemInventorySave.GetPath(pathFrom);
        var index = GetIndexByPath(path, pathIndex);
        if (index is null)
            throw new Exception("Invalid path from passed. Path from cannot be emtpy");
        _items.RemoveAt(index.Value);
    }

    public void Move(IEnumerable<string> pathFrom, int pathFromIndex, 
        IEnumerable<string> pathTo, int pathToIndex)
    {
        var pathFromString = ItemInventorySave.GetPath(pathFrom);
        var indexFrom = GetIndexByPath(pathFromString, pathFromIndex);
        if (indexFrom is null)
            throw new Exception("Invalid path from passed. Path from cannot be empty.");
        
        var pathToString = ItemInventorySave.GetPath(pathTo);
        var indexTo = GetIndexByPath(pathToString, pathToIndex);
        if (indexTo is not null)
            throw new Exception("Invalid path to passed. Path to cannot be occupied.");

        Add(new ItemInventorySave()
        {
            ItemId = _items[indexFrom.Value].ItemId,
            InventoryPath = pathTo.ToList()
        });
        Remove(pathFrom, pathFromIndex);
    }

    public void Save()
    {
#if UNITY_EDITOR
        var json = JsonConvert.SerializeObject(_items);
        var jsonBytes = Encoding.ASCII.GetBytes(json);
        var encryptedJsonBytes = EncryptHandler.Encrypt(jsonBytes);

        var savePath = AssetDatabase.GetAssetPath(_itemSaveFile);
        File.WriteAllBytes(savePath, encryptedJsonBytes);
        EditorUtility.SetDirty(_itemSaveFile);
#endif
    }

    public void ReadSave()
    {
        var jsonBytes = EncryptHandler.Decrypt(_itemSaveFile.bytes);
        var json = Encoding.ASCII.GetString(jsonBytes);
        var safeJson = json.IsNullOrEmpty() ? "[]" : json;
        _items = JsonConvert.DeserializeObject<List<ItemInventorySave>>(safeJson);
    }

    int? GetIndexByPath(string path, int pathIndex = 0)
    {
        return _items.Select((item, index) => (item, index))
            .GroupBy(t => t.item.GetPath() == path)
            .FirstOrDefault(g => g.Key)
            .Select(i => i.index)
            .Cast<int?>()
            .Skip(pathIndex)
            .FirstOrDefault();
    }
}
