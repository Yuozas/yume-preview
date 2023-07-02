using Realms;
using System;
using System.Linq;

public class RealmSaveDetails : RealmObject
{
    [PrimaryKey]
    public string SaveId { get; set; }
    public string DisplayName { get; set; }
    public DateTimeOffset Date { get; set; }
    public bool IsVisible { get; set; }

    [Backlink(nameof(ActiveRealmSaveDetails.ActiveSaveDetails))]
    public IQueryable<ActiveRealmSaveDetails> ActiveRealmSaves { get; }
}
