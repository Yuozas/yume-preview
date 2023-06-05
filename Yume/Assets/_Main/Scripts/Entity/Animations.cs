using UnityEngine;

public class Animations
{
    public const string HORIZONTAL_ANIMATOR_PARAMETER_NAME = "Horizontal";
    public const string VERTICAL_ANIMATOR_PARAMETER_NAME = "Vertical";
    public const string SPEED_ANIMATOR_PARAMETER_NAME = "Magnitude";

    private readonly Animator _animator;

    public Animations(Animator animator)
    {
        _animator = animator;
    }

    public void SetAxis(Vector2 direction)
    {
        _animator.SetFloat(HORIZONTAL_ANIMATOR_PARAMETER_NAME, direction.x);
        _animator.SetFloat(VERTICAL_ANIMATOR_PARAMETER_NAME, direction.y);
    }

    public void SetMagnitude(float magnitude)
    {
        _animator.SetFloat(SPEED_ANIMATOR_PARAMETER_NAME, magnitude);
    }
}

