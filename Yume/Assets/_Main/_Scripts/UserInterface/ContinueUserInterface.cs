using SwiftLocator.Services.ServiceLocatorServices;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class ContinueUserInterface : MonoBehaviour
{
    [field: SerializeField] private VisualTreeAsset SaveItem { get; set; }

    private IRealmSaveManager _saveManager;
    private SceneDataHandler _sceneDataHandler;
    private VisualElement _backToMenuContainer;
    private ScrollView _saveItemsScrollView;

    private void Awake()
    {
        _saveManager = ServiceLocator.SingletonProvider.Get<IRealmSaveManager>();
        _sceneDataHandler = ServiceLocator.SingletonProvider.Get<SceneDataHandler>();

        var root = GetComponent<UIDocument>().rootVisualElement;
        _backToMenuContainer = root.Q<VisualElement>("BackToMenuContainer");
        _saveItemsScrollView = root.Q<ScrollView>("SaveItemsScrollView");

        SetupBackToMenuButton();
        SetupSaveItems();
    }

    private void SetupSaveItems()
    {
        var saveItems = _saveManager.GetAllSaveDetails().Where(s => s.IsVisible);
        foreach (var saveItem in saveItems)
        {
            VisualElement saveItemContainer = SaveItem.CloneTree();
            var saveItemUserInterface = new SaveItemUserIterface(saveItemContainer, saveItem, () =>
            {
                _saveItemsScrollView.Remove(saveItemContainer);
            });
            _saveItemsScrollView.Add(saveItemContainer);
        }
    }

    private void SetupBackToMenuButton()
    {
        var backToMenuButton = _backToMenuContainer.Q<Button>("BackToMenuButton");
        backToMenuButton.clicked += TriggerBack;
    }

    private void TriggerBack()
    {
        SceneManager.LoadScene(_sceneDataHandler.MainMenuSceneName, LoadSceneMode.Single);
    }
}
