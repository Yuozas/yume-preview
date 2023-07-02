using SwiftLocator.Services.ServiceLocatorServices;
using UnityEngine;

public class TransitionTrigger : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TransitionDestinationScriptableObject _destination;

#if UNITY_EDITOR
    [SerializeField] private BoxCollider2D _collider;
#endif

    private Transitioner _transitioner;

    private void Awake()
    {
        _transitioner = ServiceLocator.GetSingleton<Transitioner>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_destination is null)
            return;

        var detected = collision.TryGetComponent<ITransitionable>(out var transitionable);
        if (!detected)
            return;

        _transitioner.Transition(_destination);
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (_collider is null)
            return;

        var colliderPosition = transform.position + (Vector3)_collider.offset;

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(colliderPosition, _collider.size);
    }
#endif
}
