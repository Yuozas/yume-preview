using UnityEngine;
using Euphelia;
public abstract class Entity : MonoBehaviour
{
    [Header("Abstract References")]
    [SerializeField] protected Animator _animator;

    [Header("Abstract Settings")]
    [SerializeField] protected Vector2 _facing = Vector2.down;

    protected Animations _animations;
    protected Direction _direction;

    protected virtual void Awake()
    {
        _animations = new Animations(_animator);
        _direction = new Direction(_animations);

        _direction.Set(_facing);
    }

    public void SetDirection(Vector2 direction) => _direction.Set(direction);
}
