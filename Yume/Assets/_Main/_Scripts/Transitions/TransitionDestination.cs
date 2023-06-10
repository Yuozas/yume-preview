using SwiftLocator.Services.ServiceLocatorServices;
using UnityEngine;

public class TransitionDestination : MonoBehaviour
{
    [field: Header("References")]
    [field: SerializeField] public Scriptable_TransitionDestination This { get; private set; }

#if UNITY_EDITOR
    [Header("Settings")]
    [SerializeField] private float _distance = 1f;
#endif
    [SerializeField] private Vector2 _direction = Vector2.up;

    private Transitioner _transitioner;

    private void Awake()
    {
        _transitioner = ServiceLocator.GetSingleton<Transitioner>();
    }

    private void OnEnable()
    {
        _transitioner.Add(this);
    }

    private void OnDisable()
    {
        _transitioner.Remove(this);
    }

    public void Transition(ITransitionable transitionable)
    {
        transitionable.Transition(transform.position, _direction);
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        OnDrawGizmosUtility.Draw(transform.position, _distance, _direction);
    }
#endif
}