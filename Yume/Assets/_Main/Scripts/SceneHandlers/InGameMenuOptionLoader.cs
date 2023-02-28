using System;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class InGameMenuOptionLoader : MonoBehaviour
{
    [SerializeField] GameObject _backpackUiGameObject;
    InGameMenuOption _inGameMenuOption;

    private void OnEnable()
    {
        EditorApplication.hierarchyChanged += OnHierarchyChanged;
    }

    private void OnDisable()
    {
        EditorApplication.hierarchyChanged -= OnHierarchyChanged;
    }

    private void OnHierarchyChanged()
    {
        if (_inGameMenuOption is not InGameMenuOption.Unset)
            return;
        if (!TryGetComponent<InGameMenuOptionHolder>(out var optionHolder))
            return;
        _inGameMenuOption = optionHolder.Option;
        LoadOption();
    }

    void LoadOption()
    {
        switch (_inGameMenuOption)
        {
            case InGameMenuOption.Backpack:
                _backpackUiGameObject.SetActive(true);
                break;  
            default:
                throw new Exception("Invalid option");
        };
    }
}
