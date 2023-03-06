using UnityEngine;

public class TransitionTrigger : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Scriptable_TransitionDestination _destination;

#if UNITY_EDITOR
    [SerializeField] BoxCollider2D _collider;
#endif

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (_destination == null) return;

        var detected = collision.TryGetComponent<Transitionable>(out var transitionable);
        if (!detected) return;

        Transitioner.Instance.Transition(_destination);
    }

    void OnDrawGizmos()
    {
        if (_collider == null) return;

        var colliderPosition = transform.position + (Vector3)_collider.offset;

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(colliderPosition, _collider.size);
    }
}
