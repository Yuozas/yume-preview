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

    [Header("Settings")]
    [SerializeField] private float _arrowOffset = 1f;

    private Decisions _decisions;
    private ChoiceGroup _group;
    private List<ChoiceUserInterface> _interfaces;

    private float _startingWidth;

    private void Awake()
    {
        _interfaces = new();
        _decisions = ServiceLocator.GetSingleton<Decisions>();
        _group = _decisions.Choices;

        _startingWidth = _holder.sizeDelta.x;
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

        UpdateHolderWidth();
        Select();
    }

    private void LateUpdate()
    {
        Select();
    }

    private void UpdateHolderWidth()
    {
        if (_interfaces.Count <= 0)
            return;

        var @interface = _interfaces
            .OrderByDescending(@interface => @interface.TextLength)
            .First();

        var width = _startingWidth + (@interface.TextLength * 5.5f);
        _holder.sizeDelta = new Vector2(width, _holder.sizeDelta.y);

        _arrow.localPosition = _arrow.localPosition.With(x: -_holder.sizeDelta.x + _arrowOffset);
    }

    private void Select()
    {
        var @interface = _interfaces.FirstOrDefault(@interface => @interface.Choice == _group.Selected);

        if(@interface is not null)
            _arrow.transform.position = _arrow.transform.position.With(y: @interface.transform.position.y);
    }

    private void Set(bool active)
    {
        if(active)
            UpdateHolderWidth();

        _visualization.SetActive(active);
    }
}
