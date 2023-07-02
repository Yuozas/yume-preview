using UnityEngine;

public class WindTurbine : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Animator _animator;

    [field: Header("Settings")]
    [field: SerializeField] public bool Activated { get; private set; }

    private const string ACTIVATED_ANIMATOR_PARAMETER = "Activated";

    private void Start()
    {
        SetParameter();
    }

    private void SetParameter()
    {
        _animator.SetBool(ACTIVATED_ANIMATOR_PARAMETER, Activated);
    }

    public void Set(bool value)
    {
        Activated = value;
        SetParameter();
    }
}
