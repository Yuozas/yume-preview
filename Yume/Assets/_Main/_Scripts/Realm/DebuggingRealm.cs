using Realms;

public class DebuggingRealm : RealmObject
{
    public string SceneName { get; set; }
    public RealmSaveDetails SaveDetails { get; set; }
}