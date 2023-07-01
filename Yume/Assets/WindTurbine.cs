using UnityEngine;

public class WindTurbine : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Animator _animator;

    [Header("Settings")]
    [SerializeField] private bool _activated;

    private const string ACTIVATED_ANIMATOR_PARAMETER = "Activated";

    private void Start()
    {
        _animator.SetBool(ACTIVATED_ANIMATOR_PARAMETER, _activated);
    }
}
