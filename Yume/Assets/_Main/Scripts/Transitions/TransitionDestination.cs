using UnityEngine;

public class TransitionDestination : MonoBehaviour
{
    [field: Header("References")]
    [field: SerializeField] public Scriptable_TransitionDestination This { get; private set; }

#if UNITY_EDITOR
    [Header("Settings")]
    [SerializeField] float _distance = 1f;
#endif
    [SerializeField] Vector2 _direction = Vector2.up;

    void OnEnable() => Transitioner.Instance.Add(this);
    void OnDisable() => Transitioner.Instance.Remove(this);
    public void Transition(ITransitionable transitionable) => transitionable.Transition(transform.position, _direction);

#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        OnDrawGizmosUtility.Draw(transform.position, _distance, _direction);
    }
#endif
}