using UnityEngine;

public class TransitionTrigger : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Scriptable_TransitionDestination _destination;
    [SerializeField] BoxCollider2D _collider;

    void OnTriggerEnter2D(Collider2D collision)
    {
        var transitionable = collision.GetComponent<Transitionable>();
        if (transitionable == null) return;

        Transitioner.Instance.Transition(_destination);
    }

    void OnDrawGizmos()
    {
        var colliderPosition = transform.position + (Vector3)_collider.offset;

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(colliderPosition, _collider.size);
    }
}
