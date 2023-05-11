using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    [Header("Abstract References")]
    [SerializeField] protected Animator _animator;

    [Header("Abstract Settings")]
    [SerializeField] protected Vector2 _direction = Vector2.down;

    public const string HORIZONTAL_ANIMATOR_PARAMETER_NAME = "Horizontal";
    public const string VERTICAL_ANIMATOR_PARAMETER_NAME = "Vertical";
    public const string SPEED_ANIMATOR_PARAMETER_NAME = "Magnitude";

    protected Vector2 _facing;

    protected virtual void Awake()
    {
        SetFacingDirection(_direction);
    }

    public void SetFacingDirection(Vector2 direction)
    {
        _facing = direction;
        SetAnimatorDirection(direction);
    }
    public void SetAnimatorDirection(Vector2 direction)
    {
        _animator.SetFloat(HORIZONTAL_ANIMATOR_PARAMETER_NAME, direction.x);
        _animator.SetFloat(VERTICAL_ANIMATOR_PARAMETER_NAME, direction.y);
    }

    protected void SetAnimatorSpeed(float magnitude)
    {
        _animator.SetFloat(SPEED_ANIMATOR_PARAMETER_NAME, magnitude);
    }
}
