public class InSceneCharacter
{
    private PlayableEntity _character;

    public PlayableEntity Get()
    {
        return _character;
    }

    public void Set(PlayableEntity character)
    {
        _character = character;
    }

    public void Remove()
    {
        _character = null;
    }
}
