public class InSceneCharacter
{
    private Character _character;

    public Character Get()
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
