using SwiftLocator.Services.ServiceLocatorServices;
using UnityEngine;
using UnityEngine.UIElements;

public class MainMenuUserInterface : MonoBehaviour
{

    void Awake()
    {
        var uiDocument = GetComponent<UIDocument>();
        var root = uiDocument.rootVisualElement;
        var saveManager = ServiceLocator.SingletonProvider.Get<ISaveManager>();
        if (!saveManager.AnySaveExists())
        {
            var continueButton = root.Q<Button>("ContinueButton");
            continueButton.SetEnabled(false);
            continueButton.AddToClassList("dim-button");
        }
    }
}
