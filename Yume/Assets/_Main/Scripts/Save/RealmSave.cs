using Realms;
using System;

public class RealmSave : RealmObject
{
    [PrimaryKey]
    public long SaveId { get; set; }
    public string DisplayName { get; set; }
    public DateTimeOffset Date { get; set; }
    public bool IsVisible { get; set; }
}