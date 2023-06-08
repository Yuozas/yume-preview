public class CharacterResolver
{
    private Character _character;

    public Character Resolve()
    {
        return _character;
    }

    public void Set(Character character)
    {
        _character = character;
    }

    public void Remove()
    {
        _character = null;
    }
}
