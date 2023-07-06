public interface IState
{
    void Enter() { }
    void Tick() { }
    void Exit() { }

    void SetReferenceToStateMachine(StateMachine states);
}