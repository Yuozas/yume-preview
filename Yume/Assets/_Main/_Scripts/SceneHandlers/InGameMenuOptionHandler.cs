using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class InGameMenuOptionHandler : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] InGameMenuOption _inGameMenuOption;
    [SerializeField] UnityEvent<InGameMenuOption> _setOption;

    public void OnPointerClick(PointerEventData eventData) => _setOption.Invoke(_inGameMenuOption);
}
