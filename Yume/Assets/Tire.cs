using UnityEngine;

public class Tire : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private EventScriptableObject _event;

    private void OnEnable()
    {
        _event.Event += Close;
    }

    private void OnDisable()
    {
        _event.Event -= Close;
    }

    private void Close()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}