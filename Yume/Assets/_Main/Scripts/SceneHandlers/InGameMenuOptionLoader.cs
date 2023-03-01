using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class InGameMenuOptionLoader : MonoBehaviour
{
    [SerializeField] int _activeCanvasLayer = 11;
    [SerializeField] int _inActiveCanvasLayer = 8;
    InGameMenuOption _inGameMenuOption;
    InGameMenuOption _previousGameMenuOption;
    [SerializeField] List<PanelOption> _uiPanelGameObjectByOption;

    Dictionary<InGameMenuOption, GameObject> _uiPanelGameObjectByOptionDictionary => 
        _uiPanelGameObjectByOption?.ToDictionary(d => d.Option, d => d.Panel);

    [SerializeField] List<CanvasOption> _optionCanvasByOption;
    Dictionary<InGameMenuOption, Canvas> _optionCanvasByOptionDictionary =>
        _optionCanvasByOption?.ToDictionary(d => d.Option, d => d.Canvas);

    public void SetOption(InGameMenuOption option)
    {
        _inGameMenuOption = option;
        LoadOption();
    }

    void OnEnable()
    {
        _inGameMenuOption = InGameMenuOption.Unset;
        _previousGameMenuOption = InGameMenuOption.Unset;
        EditorApplication.hierarchyChanged += OnHierarchyChanged;
    }

    void OnDisable()
    {
        _inGameMenuOption = InGameMenuOption.Unset;
        _previousGameMenuOption = InGameMenuOption.Unset;
        EditorApplication.hierarchyChanged -= OnHierarchyChanged;
    }

    void OnHierarchyChanged()
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
        SetActive(_inGameMenuOption);
        SetInActive(_previousGameMenuOption);
        _previousGameMenuOption = _inGameMenuOption;
    }

    void SetActive(InGameMenuOption activateOption)
    {
        if (activateOption is InGameMenuOption.Unset)
            return;
        var gameObject = _uiPanelGameObjectByOptionDictionary[activateOption];
        gameObject.SetActive(true);
        var canvas = _optionCanvasByOptionDictionary[activateOption];
        canvas.sortingOrder = _activeCanvasLayer;
    }

    void SetInActive(InGameMenuOption inActiveOption)
    {
        if (inActiveOption is InGameMenuOption.Unset)
            return;

        var gameObject = _uiPanelGameObjectByOptionDictionary[inActiveOption];
        gameObject.SetActive(false);
        var canvas = _optionCanvasByOptionDictionary[inActiveOption];
        canvas.sortingOrder = _inActiveCanvasLayer;
    }

    [Serializable]
    public class PanelOption
    {
        [field: SerializeField] public InGameMenuOption Option { get; set; }
        [field: SerializeField] public GameObject Panel { get; set; }
    }

    [Serializable]
    public class CanvasOption
    {
        [field:SerializeField] public InGameMenuOption Option { get; set; }
        [field: SerializeField] public Canvas Canvas { get; set; }
    }
}
