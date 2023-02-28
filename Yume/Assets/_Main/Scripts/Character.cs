using UnityEngine;
using UnityEngine.InputSystem;

public class Character : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Rigidbody2D _rigidbody;
    [SerializeField] Animator _animator;

    public const float BASE_MOVEMENT_SPEED = 5f;
    public const string HORIZONTAL_AXIS_NAME = "Horizontal";
    public const string VERTICAL_AXIS_NAME = "Vertical";

    public const string HORIZONTAL_ANIMATOR_PARAMETER_NAME = "Horizontal";
    public const string VERTICAL_ANIMATOR_PARAMETER_NAME = "Vertical";
    public const string SPEED_ANIMATOR_PARAMETER_NAME = "Magnitude";

    Vector2 _axis;
    public void SetAxis(InputAction.CallbackContext context) => _axis = context.ReadValue<Vector2>();
    public void SetFacing(Vector2 direction)
    {
        _animator.SetFloat(HORIZONTAL_ANIMATOR_PARAMETER_NAME, direction.x);
        _animator.SetFloat(VERTICAL_ANIMATOR_PARAMETER_NAME, direction.y);
    }

    void Update()
    {
        var velocity = _axis.normalized * BASE_MOVEMENT_SPEED;
        _rigidbody.velocity = velocity;

        var magnitude = velocity.magnitude;
        if (magnitude >= Mathf.Epsilon) SetFacing(_axis.normalized);
        SetMagnitude(magnitude);
    }

    void SetMagnitude(float magnitude) => _animator.SetFloat(SPEED_ANIMATOR_PARAMETER_NAME, magnitude);
}
