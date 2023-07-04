using UnityEngine;
using TMPro;

public class SelectableQuestUserInterface : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private CanvasGroup _group;
    [SerializeField] private TMP_Text _text;

    public QuestScriptableObject Quest { get; private set; }
    private QuestsUserInterface _quests;

    public void Initialize(QuestsUserInterface quests, QuestScriptableObject quest)
    {
        _quests = quests;
        Quest = quest;
        Quest.OnCompleted += UpdateVisualization;

        UpdateVisualization();
    }

    private void OnDestroy()
    {
        Quest.OnCompleted -= UpdateVisualization;
    }

    public void Button_Select()
    {
        _quests.Select(this);
    }

    private void UpdateVisualization()
    {
        UpdateTransparency();
        UpdateTitle();
    }

    private void UpdateTransparency()
    {
        const float completedTransparency = 0.5f;
        const float uncompletedTransparency = 1f;
        _group.alpha = Quest.Completed ? completedTransparency : uncompletedTransparency;
    }

    private void UpdateTitle()
    {
        _text.text = Quest.Title;
    }
}
