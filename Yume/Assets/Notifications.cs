using System;

public class Notifications
{
    public event Action<string> OnAdded;

    public const string INVENTORY_UPDATED = "Inventory updated";
    public const string QUESTS_UPDATED = "Quests updated";

    public void Add(string name)
    {
        OnAdded?.Invoke(name);
    }
}
