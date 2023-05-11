using UnityEngine;
using UnityEngine.InputSystem;

public class Character : Entity
{
    [Header("References")]
    [SerializeField] private Rigidbody2D _rigidbody;

    public const float BASE_MOVEMENT_SPEED = 5f;
    public const float INTERACT_DISTANCE = 1f;

    private Vector2 _axis;

    protected override void Awake()
    {
        base.Awake();
        Physics2D.queriesStartInColliders = false;
    }

    private void Update()
    {
        Movement();
    }

    public void Interact()
    {
        var hit = Physics2D.Raycast(transform.position, _facing, INTERACT_DISTANCE);

        if (hit.collider is null)
            return;

        var found = hit.collider.TryGetComponent<Conversation>(out var conversation);
        if (!found)
            return;

        conversation.Interact();
    }
    public void SetAxis(InputAction.CallbackContext context) => _axis = context.ReadValue<Vector2>();

    private void Movement()
    {
        var velocity = _axis.normalized * BASE_MOVEMENT_SPEED;
        _rigidbody.velocity = velocity;

        var magnitude = velocity.magnitude;
        if (magnitude >= Mathf.Epsilon)
            SetFacingDirection(_axis.normalized);

        SetAnimatorSpeed(magnitude);
    }
}
