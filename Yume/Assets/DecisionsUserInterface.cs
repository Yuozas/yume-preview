using SwiftLocator.Services.ServiceLocatorServices;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class DecisionsUserInterface : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject _visualization;
    [SerializeField] private ChoiceUserInterface _prefab;
    [SerializeField] private RectTransform _holder;
    [SerializeField] private RectTransform _arrow;

    private Decisions _decisions;
    private ChoiceGroup _group;
    private List<ChoiceUserInterface> _interfaces;

    private void Awake()
    {
        _interfaces = new();
        _decisions = ServiceLocator.GetSingleton<Decisions>();
        _group = _decisions.Choices;
    }

    private void OnEnable()
    {
        _group.OnUpdated += Populate;
        _group.OnSelected += Select;

        _decisions.Toggler.OnUpdated += Set;
    }

    private void OnDisable()
    {
        _group.OnUpdated -= Populate;
        _group.OnSelected -= Select;

        _decisions.Toggler.OnUpdated -= Set;
    }

    private void Clear()
    {
        foreach (var @interface in _interfaces)
            Destroy(@interface.gameObject);

        _interfaces.Clear();
    }

    private void Populate()
    {
        Clear();

        foreach (var choice in _group.Choices)
        {
            var instantiated = Instantiate(_prefab, _holder);
            instantiated.Initialize(choice);
            _interfaces.Add(instantiated);
        }
    }

    private void Select()
    {
        var @interface = _interfaces.First(@interface => @interface.Choice == _group.Selected);
        _arrow.transform.position = _arrow.transform.position.With(y: @interface.transform.position.y);
    }

    private void Set(bool active)
    {
        _visualization.SetActive(active);
    }
}
