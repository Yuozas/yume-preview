using Unity.Cinemachine;
using UnityEngine;

public class CustomCinemachineUpdate : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private CinemachineBrain _brain;

    void Start()
    {
        _brain.ManualUpdate();
    }
}
