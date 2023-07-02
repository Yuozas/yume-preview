using SwiftLocator.Services.ServiceLocatorServices;
using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayableEntity : Entity, ITransitionable
{
    [Header("References")]
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private BoxCollider2D _collider;

    private States _states;
    private InputActions _input;
    private InSceneCharacter _characterResolver;
    private DialogueResolver _dialogueResolver;
    private Decisions _decisions;
    private SliderGame _slider;

    protected override void Awake()
    {
        base.Awake();

        CreateAndAssignPhysicsMaterial();

        _input = new InputActions();

        var movement = new Movement(_rigidbody, _animations, _direction);
        var interaction = new MultipleInteractor();

        Physics2D.queriesStartInColliders = false;

        AddStates(movement, interaction);
    }

    private void OnDestroy()
    {
        _characterResolver.Remove();
    }

    private void OnEnable()
    {
        _input.Enable();
    }

    private void OnDisable()
    {
        _input.Disable();
    }

    private void Start()
    {
        _decisions = ServiceLocator.GetSingleton<Decisions>();
        _dialogueResolver = ServiceLocator.GetSingleton<DialogueResolver>();
        _slider = ServiceLocator.GetSingleton<SliderGame>();

        _characterResolver = ServiceLocator.GetSingleton<InSceneCharacter>();
        _characterResolver.Set(this);
        _states.Set<Walking>();
    }

    private void Update()
    {
        _states.Tick();
    }

    public void Transition(Vector3 position, Vector2 direction)
    {
        transform.position = position;
        SetDirection(direction);
    }

    private void AddStates(Movement movement, MultipleInteractor interaction)
    {
        var talkingTransitions = new Dictionary<Func<bool>, Type>()
        {
            [ToWalking] = typeof(Walking),
            [ToChoosing] = typeof(Choosing),
            [ToSlider] = typeof(PlayingSliderGame),
        };

        var walkingTransitions = new Dictionary<Func<bool>, Type>()
        {
            [ToTalking] = typeof(Talking),
            [ToChoosing] = typeof(Choosing),
            [ToSlider] = typeof(PlayingSliderGame),
        };

        var choosingTransitions = new Dictionary<Func<bool>, Type>()
        {
            [ToTalking] = typeof(Talking),
            [ToWalking] = typeof(Walking),
            [ToSlider] = typeof(PlayingSliderGame),
        };

        var sliderGameTransitions = new Dictionary<Func<bool>, Type>()
        {
            [ToTalking] = typeof(Talking),
            [ToWalking] = typeof(Walking),
            [ToChoosing] = typeof(Choosing)
        };

        var states = new IState[]
        {
            new Walking(_input.Walking, movement, _direction, interaction, walkingTransitions),
            new Choosing(_input.Choosing, choosingTransitions),
            new PlayingSliderGame(_input.Slider, sliderGameTransitions),
            new Talking(_input.Talking, talkingTransitions)
        };

        _states = new States(states);
    }

    private bool ToWalking()
    {
        var dialogues = _dialogueResolver.Resolve();
        return dialogues.All(dialogue => !dialogue.Toggler.Enabled) && !_decisions.Toggler.Enabled && !_slider.Toggler.Enabled;
    }

    private bool ToSlider()
    {
        return _slider.Toggler.Enabled;
    }

    private bool ToTalking()
    {
        var dialogues = _dialogueResolver.Resolve();
        return dialogues.Any(dialogue => dialogue.Toggler.Enabled) && !_decisions.Toggler.Enabled && !_slider.Toggler.Enabled;
    }

    private bool ToChoosing()
    {
        return _decisions.Toggler.Enabled && !_slider.Toggler.Enabled;
    }

    private void CreateAndAssignPhysicsMaterial()
    {
        const string name = "Physics";
        var material = new PhysicsMaterial2D(name)
        {
            friction = 0,
            bounciness = 0
        };

        _rigidbody.sharedMaterial = material;
        _collider.sharedMaterial = material;
    }
}