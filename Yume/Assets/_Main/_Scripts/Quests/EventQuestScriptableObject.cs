using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Quests/Event Quest")]
public class EventQuestScriptableObject : QuestScriptableObject
{
    [Header("References")]
    [SerializeField] private EventScriptableObject _event;

    [Header("Settings")]
    [SerializeField] private string _title;
    [SerializeField, TextArea] private string _description;

    public override string Title => _title;
    public override string Description => _description;

    public override void Initialize()
    {
        _event.Event += SetCompletedAndUnsubscribe;
        if (_event.Invoked)
            SetCompleted();
    }

    private void SetCompletedAndUnsubscribe()
    {
        _event.Event -= SetCompletedAndUnsubscribe;
        SetCompleted();
    }
}
