using UnityEngine;

public class TransitionDestination : MonoBehaviour
{
    [field: Header("References")]
    [field: SerializeField] public Scriptable_TransitionDestination This { get; private set; }

#if UNITY_EDITOR
    [Header("Settings")]
    [SerializeField] float _distance = 1f;
    [SerializeField] Vector2 _direction = Vector2.up;
#endif

    void OnEnable() => Transitioner.Instance.Add(this);
    void OnDisable() => Transitioner.Instance.Remove(this);
    public void Set(Transitionable transitionable) => transitionable.Set(transform.position);

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        OnDrawGizmosUtility.Draw(transform.position, _distance, _direction);
    }
}