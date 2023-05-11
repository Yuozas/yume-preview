public class States
{
    IState _current;
    public void Set(IState state)
    {
        _current?.Exit();

        _current = state;
        _current.Enter();
    }

    public void Tick()
    {
        _current.Tick();
    }
}
