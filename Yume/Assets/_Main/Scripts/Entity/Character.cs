using SwiftLocator.Services.ServiceLocatorServices;
using UnityEngine;

public class Character : Entity, ITransitionable
{
    [Header("References")]
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private BoxCollider2D _collider;

    private States _states;
    private InputActions _input;
    private InSceneCharacter _resolver;

    protected override void Awake()
    {
        base.Awake();

        CreateAndAssignPhysicsMaterial();

        _resolver = ServiceLocator.GetSingleton<InSceneCharacter>();
        _resolver.Set(this);

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

    private void OnDestroy()
    {
        _resolver.Remove();
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
        _states.Set<Walking>();
    }

    private void Update()
    {
        _states.Tick();
    }

    public void Transition(Vector3 position, Vector2 direction)
    {
        transform.position = position;
        SetDirection(direction);
    }

    private void Set()
    {
        _states.Set<Talking>();
    }

    private void CreateAndAssignPhysicsMaterial()
    {
        var name = "Physics";
        var material = new PhysicsMaterial2D(name)
        {
            friction = 0,
            bounciness = 0
        };

        _rigidbody.sharedMaterial = material;
        _collider.sharedMaterial = material;
    }
}