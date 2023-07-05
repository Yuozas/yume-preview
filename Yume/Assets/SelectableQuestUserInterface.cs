using UnityEngine;
using TMPro;

public class SelectableQuestUserInterface : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private CanvasGroup _group;
    [SerializeField] private TMP_Text _text;

    public QuestScriptableObject Quest { get; private set; }

    public void Initialize(QuestScriptableObject quest)
    {
        Quest = quest;
        Quest.OnCompleted += UpdateVisualization;

        UpdateVisualization();
    }

    private void OnDestroy()
    {
        Quest.OnCompleted -= UpdateVisualization;
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
