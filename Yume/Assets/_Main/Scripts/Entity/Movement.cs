using UnityEngine;

public class Movement
{
    public const float DEFAULT_MOVEMENT_SPEED = 5f;
    public Vector2 Position => _rigidbody.transform.position;

    private readonly Rigidbody2D _rigidbody;
    private readonly Animations _animations;
    private readonly Direction _direction;

    public Movement(Rigidbody2D rigidbody, Animations animations, Direction direction)
    {
        _rigidbody = rigidbody;
        _animations = animations;
        _direction = direction;
    }

    public void Tick(Vector2 direction, float speed)
    {
        var velocity = direction.normalized * speed;
        _rigidbody.velocity = velocity;

        var magnitude = velocity.magnitude;
        if (magnitude >= Mathf.Epsilon)
            _direction.Set(direction.normalized);

        _animations.SetMagnitude(magnitude);
    }
}

