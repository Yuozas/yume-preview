using System;
using System.Collections.Generic;
using System.Linq;

public class CharacterDataHandler
{
    public readonly int EmberCharacterId = 0;
    public readonly int AuraCharacterId = 1;

    private readonly Dictionary<int, DefaultCharacterData> _characters;
    public CharacterDataHandler(SceneDataHandler sceneDataHandler)
    {
        _characters = new Dictionary<int, DefaultCharacterData>()
        {
            [EmberCharacterId] = new DefaultCharacterData(EmberCharacterId, DefaultCharacterData.MAIN_TYPE, "Ember", sceneDataHandler.DemoSceneName),
            [AuraCharacterId] = new DefaultCharacterData(AuraCharacterId, DefaultCharacterData.MAIN_TYPE, "Aura", sceneDataHandler.DemoSceneName)
        };
        AddCharacter(new DefaultCharacterData(DefaultCharacterData.SUPPORT_TYPE, "Hazel"));
        AddCharacter(new DefaultCharacterData(DefaultCharacterData.SUPPORT_TYPE, "Nova"));
        AddCharacter(new DefaultCharacterData(DefaultCharacterData.SUPPORT_TYPE, "Clar"));
    }

    public DefaultCharacterData GetCharacterData(int id)
    {
        if(!_characters.ContainsKey(id))
            throw new ArgumentException($"Character with id {id} does not exist.");

        return _characters[id];
    }

    public DefaultCharacterData GetFirstCharacterWhere(Func<DefaultCharacterData, bool> predicate)
    {
        return _characters.Values.First(predicate);
    }

    /// <summary>
    ///     Adds a character to the dictionary with automatically assigned id.
    /// </summary>
    private void AddCharacter(DefaultCharacterData character) 
    {
        var newCharacterId = _characters.Values.Last().Id + 1;
        var newCharacter = new DefaultCharacterData(newCharacterId, character);
        _characters.Add(newCharacter.Id, newCharacter);
    }
}