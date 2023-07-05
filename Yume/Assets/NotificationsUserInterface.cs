using SwiftLocator.Services.ServiceLocatorServices;
using UnityEngine;

public class NotificationsUserInterface : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private NotificationUserInterface _prefab;
    [SerializeField] private RectTransform _holder;

    private Notifications _notifications;

    private void Awake()
    {
        _notifications = ServiceLocator.GetSingleton<Notifications>();
    }

    private void OnEnable()
    {
        _notifications.OnAdded += Add;
    }

    private void OnDisable()
    {
        _notifications.OnAdded -= Add;
    }

    private void Add(string name)
    {
        var instantiated = Instantiate(_prefab, _holder);
        instantiated.Initialize(name);
    }
}