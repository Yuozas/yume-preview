using UnityEngine;

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

    void Update()
    {
        var horizontal = Input.GetAxisRaw("Horizontal");
        var vertical = Input.GetAxisRaw("Vertical");

        var direction = Vector2.right * horizontal + Vector2.up * vertical;
        var velocity = direction.normalized * BASE_MOVEMENT_SPEED;
        _rigidbody.velocity = velocity;

        Animate(horizontal, vertical, velocity);
    }

    void Animate(float horizontal, float vertical, Vector2 velocity)
    {
        var magnitude = velocity.magnitude;
        if (magnitude >= Mathf.Epsilon)
        {
            _animator.SetFloat(HORIZONTAL_ANIMATOR_PARAMETER_NAME, horizontal);
            _animator.SetFloat(VERTICAL_ANIMATOR_PARAMETER_NAME, vertical);
        }
        _animator.SetFloat(SPEED_ANIMATOR_PARAMETER_NAME, magnitude);
    }
}
