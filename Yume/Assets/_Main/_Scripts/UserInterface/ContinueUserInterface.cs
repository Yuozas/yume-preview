using SwiftLocator.Services.ServiceLocatorServices;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class ContinueUserInterface : MonoBehaviour
{
    [field: SerializeField] private VisualTreeAsset SaveItem { get; set; }

    private IRealmSaveManager _saveManager;
    private VisualElement _backToMenuContainer;
    private ScrollView _saveItemsScrollView;
    private IRealmSaveReadHelper _realmSaveReadHelper;

    private void Awake()
    {
        _realmSaveReadHelper = ServiceLocator.GetSingleton<IRealmSaveReadHelper>();

        var root = GetComponent<UIDocument>().rootVisualElement;
        _backToMenuContainer = root.Q<VisualElement>("BackToMenuContainer");
        _saveItemsScrollView = root.Q<ScrollView>("SaveItemsScrollView");

        SetupBackToMenuButton();
        SetupSaveItems();
    }

    private void SetupSaveItems()
    {
        var saveItems = _realmSaveReadHelper.GetAllSaveDetails().Where(s => s.IsVisible);
        foreach (var saveItem in saveItems)
        {
            var saveItemContainer = SaveItem.CloneTree();
            var saveItemUserInterface = new SaveItemUserIterface(saveItemContainer, saveItem, () =>
            {
                _saveItemsScrollView.Remove(saveItemContainer);
            });
            _saveItemsScrollView.Add(saveItemContainer);
        }
    }

    private void SetupBackToMenuButton()
    {
        _backToMenuContainer.Q<Button>("BackToMenuButton").clicked += TriggerBack;
    }

    private void TriggerBack()
    {
        SceneManager.LoadScene(Scene.MainMenuScene.Name, LoadSceneMode.Single);
    }
}
