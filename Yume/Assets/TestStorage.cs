using SwiftLocator.Services.ServiceLocatorServices;
using UnityEngine;

public class TestStorage : MonoBehaviour
{
    [field: SerializeField] private StorageScriptableObject StorageScriptableObject { get; set; }

    private BackpackAndStorageUserInterface _backPackStoargeUserInterface;

    private void Start()
    {
        _backPackStoargeUserInterface = ServiceLocator.GetSingleton<BackpackAndStorageUserInterface>();

        var storage = ServiceLocator.GetSingleton<IStorageManager>().GetCreateStorage(StorageScriptableObject);

        _backPackStoargeUserInterface.Enter(storage.Result.Id);
    }
}
