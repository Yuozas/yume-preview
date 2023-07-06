using SwiftLocator.Services.ServiceLocatorServices;
using UnityEngine;
using System.Collections.Generic;
using System;

public class QuestsUserInterface : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject _visualization;
    [SerializeField] private SelectedQuestUserInterface _selectedWindow;
    [SerializeField] private SelectableQuestUserInterface _prefab;
    [SerializeField] private RectTransform _holder;
    [SerializeField] private Transform _arrow;

    private Quests _quests;
    private List<SelectableQuestUserInterface> _selectables;
    private SelectableQuestUserInterface _selectedQuest;
    private IToggler _toggler;

    private void Awake()
    {
        _selectables = new();
        _quests = ServiceLocator.GetSingleton<Quests>();
        _quests.OnAdded += InstantiateAndAddElement;
        _quests.OnSelectedQuestIndexUpdated += SelectElementByIndex;

        _toggler = _quests.Toggler;
        SetElementVisibility(_toggler.Enabled);
        _toggler.OnUpdated += SetElementVisibility;

        InstantiateAndAddElements();
    }

    private void OnDestroy()
    {
        _quests.OnAdded -= InstantiateAndAddElement;
        _quests.OnSelectedQuestIndexUpdated -= SelectElementByIndex;
        _toggler.OnUpdated -= SetElementVisibility;
    }

    public void Button_Quit()
    {
        _quests.Toggler.Disable();
    }

    private void SelectElementByIndex(int index)
    {
        UpdateSelectedElementAndInformationWindow(_selectables[index]);
    }

    public void UpdateSelectedElementAndInformationWindow(SelectableQuestUserInterface selected)
    {
        _selectedQuest = selected;
        if(_selectedQuest is null)
        {
            _selectedWindow.gameObject.SetActive(false);
            return;
        }

        _selectedWindow.gameObject.SetActive(true);
        _selectedWindow.Initialize(selected.Quest);
    }

    private void Update()
    {
        _arrow.gameObject.SetActive(_selectedQuest is not null);
        if(_selectedQuest is not null)
            _arrow.transform.position = _arrow.transform.position.With(y: _selectedQuest.transform.position.y);
    }

    private void InstantiateAndAddElements()
    {
        RemoveElements();

        var entries = _quests.Entries;
        foreach (var entry in entries)
            InstantiateAndAddElement(entry);

        var selectable = _selectables.Count is 0 ? null : _selectables[0];
        UpdateSelectedElementAndInformationWindow(selectable);
    }

    private void InstantiateAndAddElement(QuestScriptableObject entry)
    {
        var instantiated = Instantiate(_prefab, _holder);
        instantiated.Initialize(entry);

        _selectables.Add(instantiated);
    }

    private void RemoveElements()
    {
        foreach (var selectable in _selectables)
            Destroy(selectable.gameObject);

        _selectables.Clear();
    }

    private void SetElementVisibility(bool visible)
    {
        _visualization.SetActive(visible);
    }
}
