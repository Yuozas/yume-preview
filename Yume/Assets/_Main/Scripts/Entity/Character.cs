using UnityEngine;

public class Character : Entity, ITransitionable
{
    [Header("References")]
    [SerializeField] private Rigidbody2D _rigidbody;

    private States _states;
    private InputActions _input;

    protected override void Awake()
    {
        base.Awake();
        _input = new InputActions();

        var movement = new Movement(_rigidbody, _animations, _direction);
        var interaction = new Interactor();

        Physics2D.queriesStartInColliders = false;

        var talking = new Talking(_input.Talking);
        var states = new IState[]
        {
            new Walking(_input.Walking, movement, _direction, interaction),
            talking
        };

        _states = new States(states);
        talking.Set(_states);
    }

    private void OnEnable()
    {
        _input.Enable();
        Dialogue.Enabled += Set;
    }

    private void OnDisable()
    {
        _input.Disable();
        Dialogue.Enabled -= Set;
    }

    private void Start()
    {
        var starting = typeof(Walking);
        _states.Set(starting);
    }

    private void Update()
    {
        _states.Tick();
    }

    private void Set()
    {
        var type = typeof(Talking);
        _states.Set(type);
    }

    public void Transition(Vector3 position, Vector2 direction)
    {
        transform.position = position;
        SetDirection(direction);
    }
}