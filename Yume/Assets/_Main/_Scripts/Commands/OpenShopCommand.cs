using SwiftLocator.Services.ServiceLocatorServices;
using System;
using UnityEngine;

[Serializable]
public class OpenShopCommand : ICommand
{
    [SerializeField] public StoreScriptableObject StoreScriptableObject;

    public OpenShopCommand(StoreScriptableObject storeScriptableObject = null)
    {
        StoreScriptableObject = storeScriptableObject;
    }

    public void Execute(Action onFinished = null)
    {
        var store = ServiceLocator.GetSingleton<IStoreManager>().GetCreateStore(StoreScriptableObject);
        ServiceLocator.GetSingleton<StoreUserInterface>().Enter(store.Result.Id);
        onFinished?.Invoke();
    }
}