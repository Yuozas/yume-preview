using UnityEngine;
using Euphelia;

public class Movement
{
    public const float DEFAULT_SPEED = 5f;
    public Vector2 Position => _rigidbody.transform.position;

    private readonly Rigidbody2D _rigidbody;
    private readonly Animations _animations;
    private readonly Direction _direction;

    private Vector2 _axis;

    public Movement(Rigidbody2D rigidbody, Animations animations, Direction direction)
    {
        _rigidbody = rigidbody;
        _animations = animations;
        _direction = direction;
    }

    public void Set(Vector2 axis)
    {
        _axis = axis;
    }

    public void Tick(float speed)
    {
        var velocity = _axis.normalized * speed;
        _rigidbody.velocity = velocity;

        var magnitude = velocity.magnitude;
        if (magnitude >= Mathf.Epsilon)
            _direction.Set(_axis.normalized);

        _animations.SetMagnitude(magnitude);
    }
}

