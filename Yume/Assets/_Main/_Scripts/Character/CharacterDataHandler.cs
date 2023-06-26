using Realms;

public class CharacterDataHandler
{
    private readonly IRealmContext _realmContext;

    public CharacterDataHandler(IRealmContext realmContext)
    {
        _realmContext = realmContext;
        AddCharacterRelatedData();
    }

    private void AddCharacterRelatedData()
    {
        using var realm = _realmContext.GetGlobalRealm();
        using var transaction = realm.BeginWrite();

        AddCharacterTypes(realm);
        AddCharacters(realm);

        transaction.Commit();
    }

    private void AddCharacters(Realm realm)
    {
        var characters = Character.AllCharacters.Values;
        foreach (var character in characters)
            realm.Add<CharacterRealmObject>(character, true);
    }

    private void AddCharacterTypes(Realm realm)
    {
        var characterTypes = CharacterType.AllTypes.Values;
        foreach (var characterType in characterTypes)
            realm.Add<CharacterTypeRealmObject>(characterType, true);
    }
}