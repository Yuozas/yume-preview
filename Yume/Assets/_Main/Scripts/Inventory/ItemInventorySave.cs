using System.Collections.Generic;

public class ItemInventorySave
{
    public string ItemId { get; set; }
    public List<string> InventoryPath { get; set; }

    public string GetPath() => GetPath(InventoryPath);
    public static string GetPath(IEnumerable<string> path) => string.Join('/', path);
}
