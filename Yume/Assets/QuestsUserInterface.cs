using SwiftLocator.Services.ServiceLocatorServices;
using UnityEngine;
using System.Collections.Generic;

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
        _quests.OnAdded += InstantiateAndAdd;

        _toggler = _quests.Toggler;
        Set(_toggler.Enabled);
        _toggler.OnUpdated += Set;

        Populate();
    }

    private void OnDestroy()
    {
        _quests.OnAdded -= InstantiateAndAdd;
        _toggler.OnUpdated -= Set;
    }

    public void Select(SelectableQuestUserInterface selected)
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

    private void Populate()
    {
        Clear();

        var entries = _quests.Entries;
        foreach (var entry in entries)
            InstantiateAndAdd(entry);

        var selectable = _selectables.Count is 0? null : _selectables[0];
        Select(selectable);
    }

    private void InstantiateAndAdd(QuestScriptableObject entry)
    {
        var instantiated = Instantiate(_prefab, _holder);
        instantiated.Initialize(this, entry);

        _selectables.Add(instantiated);
    }

    private void Clear()
    {
        foreach (var selectable in _selectables)
            Destroy(selectable.gameObject);

        _selectables.Clear();
    }

    private void Set(bool active)
    {
        _visualization.SetActive(active);
    }
}
