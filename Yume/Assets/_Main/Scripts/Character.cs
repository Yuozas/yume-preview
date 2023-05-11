using UnityEngine;

public class Character : Entity
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
        var interaction = new Interaction();

        Physics2D.queriesStartInColliders = false;

        var states = new IState[]
        {
            new Walking(_input, movement, _direction, interaction),
            new Talking(_input)
        };


        _states = new States(states);
    }

    private void OnEnable()
    {
        _input.Enable();
    }

    private void OnDisable()
    {
        _input.Disable();
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
}