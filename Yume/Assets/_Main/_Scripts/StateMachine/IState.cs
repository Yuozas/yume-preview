public interface IState
{
    void Enter() { }
    void Tick() { }
    void Exit() { }

    void Set(States states);
}