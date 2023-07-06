using SwiftLocator.Services.ServiceLocatorServices;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class NotificationsUserInterface : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private NotificationUserInterface _prefab;
    [SerializeField] private RectTransform _holder;

    private Notifications _notifications;
    private List<NotificationUserInterface> _elements;

    private void Awake()
    {
        _elements = new();
        _notifications = ServiceLocator.GetSingleton<Notifications>();
    }

    private void OnEnable()
    {
        _notifications.OnAdded += AddElement;
    }

    private void OnDisable()
    {
        _notifications.OnAdded -= AddElement;
    }

    private void AddElement(string name)
    {
        var contains = _elements.Any(element => name == element.Name);
        if (contains)
            return;

        var instantiated = Instantiate(_prefab, _holder);
        _elements.Add(instantiated);

        instantiated.Initialize(name, RemoveElement);
    }

    private void RemoveElement(NotificationUserInterface element)
    {
        var contains = ContainsElement(element);
        if (contains)
            _elements.Remove(element);
    }

    private bool ContainsElement(NotificationUserInterface element)
    {
        return _elements.Contains(element);
    }
}