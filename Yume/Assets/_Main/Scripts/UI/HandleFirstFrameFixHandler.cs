using UnityEngine;
using UnityEngine.UI;

public class HandleFirstFrameFixHandler : MonoBehaviour
{
    [SerializeField] Scrollbar _scrollbar;

    private void Start() => _scrollbar.value = 0.999f;
}
