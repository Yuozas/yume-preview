public interface IRealmSaveReadHelper
{
    bool AnySaveExists();
    RealmSaveDetails[] GetAllSaveDetails();
}