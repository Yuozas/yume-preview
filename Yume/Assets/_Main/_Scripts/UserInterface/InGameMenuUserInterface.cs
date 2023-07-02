using SwiftLocator.Services.ServiceLocatorServices;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class InGameMenuUserInterface : MonoBehaviour
{
    [field: SerializeField] private InGameMenuUserInterfaceScriptableObject BackpackUserIterfaceScriptableObject { get; set; }
    [field: SerializeField] private InGameMenuUserInterfaceScriptableObject SettingsUserIterfaceScriptableObject { get; set; }

    private const int BACKPACK_INDEX = 0;
    private const int SETTINGS_INDEX = 1;
    private readonly int[] _keys = new[] { BACKPACK_INDEX, SETTINGS_INDEX };
    private Dictionary<int, VisualElement> _menuIconContainers;
    private Dictionary<int, InGameMenuUserInterfaceScriptableObject> _scriptableObjects;
    private InputActions _inputActions;
    private int? _activeIndex;
    private int? _hoverIndex;
    private VisualElement _root;

    public void EnterBackpack()
    {
        if (_activeIndex == BACKPACK_INDEX)
        {
            Exit();
            return;
        }
        Enter();
        Switch(BackpackUserIterfaceScriptableObject, BACKPACK_INDEX);
    }

    public void EnterSettings()
    {
        if (_activeIndex == SETTINGS_INDEX)
        {
            Exit();
            return;
        }
        Enter();
        Switch(SettingsUserIterfaceScriptableObject, SETTINGS_INDEX);
    }

    public void Exit()
    {
        _activeIndex = null;
        _root.style.display = DisplayStyle.None;
        _inputActions.IngameMenu.Disable();
        ServiceLocator.GetSingleton<InGameMenuLaunchHandler>().Enter();
    }

    private void Enter()
    {
        _root.style.display = DisplayStyle.Flex;
        _inputActions.IngameMenu.Enable();
        _inputActions.IngameMenu.Backpackmenu.performed += _ => EnterBackpack();
        _inputActions.IngameMenu.Exit.performed += _ => Exit();
        _inputActions.IngameMenu.Settingsmenu.performed += _ => EnterSettings();
    }

    private void Start()
    {
        _root = GetComponent<UIDocument>().rootVisualElement;

        var body = _root.Q<VisualElement>("InGameMenu");
        var menu = body.Q<VisualElement>("Menu");
        var menuIconConainers = menu.Query<VisualElement>("MenuIconAbsoluteContainer").ToList();
        _inputActions = new InputActions();

        _scriptableObjects = new Dictionary<int, InGameMenuUserInterfaceScriptableObject>
        {
            [BACKPACK_INDEX] = BackpackUserIterfaceScriptableObject,
            [SETTINGS_INDEX] = SettingsUserIterfaceScriptableObject
        };
        _menuIconContainers = new Dictionary<int, VisualElement>
        {
            [BACKPACK_INDEX] = menuIconConainers[0],
            [SETTINGS_INDEX] = menuIconConainers[1]
        };

        foreach (var key in _keys)
        {
            var iconContainer = _menuIconContainers[key];
            var scriptableObject = _scriptableObjects[key];
            scriptableObject.SetupIcon(iconContainer.Q<VisualElement>("Icon"));
            iconContainer.RegisterCallback<MouseEnterEvent>(e => MouseEnterIcon(key));
            iconContainer.RegisterCallback<MouseLeaveEvent>(e => MouseLeaveIcon());
            iconContainer.RegisterCallback<MouseDownEvent>(e => Switch(scriptableObject, key));
        }
        Exit();
    }

    private void Switch(InGameMenuUserInterfaceScriptableObject inGameMenu, int index)
    {
        if(index == _activeIndex)
            return;

        _activeIndex = index;
        inGameMenu.SetupMenuElement("MenuContent", _root);
        SwitchActiveIcon(index);
    }

    private void MouseEnterIcon(int index)
    {
        if (index == _activeIndex)
            return;

        SwitchActiveIcon(index);
    }

    private void MouseLeaveIcon()
    {
        if (_hoverIndex == _activeIndex) 
            _hoverIndex = null;
        else
            SwitchActiveIcon(_activeIndex.Value);
    }

    private void SwitchActiveIcon(int newIndex)
    {
        foreach (var icon in _menuIconContainers.Values)
            icon.RemoveFromClassList("active");
        _menuIconContainers[newIndex].AddToClassList("active");
    }
}