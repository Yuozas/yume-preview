using System.Linq;

public class States
{
    private IState _current;
    private readonly IState[] _states;

    public States(IState[] states)
    {
        _states = states;
    }

    public void Set(IState state)
    {
        _current?.Exit();

        _current = state;
        _current?.Enter();
    }

    public void Set<T>() where T : IState
    {
        var state = _states.OfType<T>().First();
        Set(state);
    }

    public void Tick()
    {
        _current?.Tick();
    }
}