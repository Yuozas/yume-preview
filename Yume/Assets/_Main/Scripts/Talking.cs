using UnityEngine.InputSystem;

public class Talking : IState
{
    private readonly InputActions.TalkingActions _talking;
    public Talking(InputActions input)
    {
        _talking = input.Talking;
    }
    public void Enter()
    {
        _talking.Enable();
        _talking.Interact.performed += Interact;
    }
    public void Exit()
    {
        _talking.Disable();
        _talking.Interact.performed -= Interact;
    }
    private void Interact(InputAction.CallbackContext context)
    {
        
    }


    public void Tick() { }


}

