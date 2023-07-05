using UnityEngine;
using TMPro;
using System;

public class SelectedQuestUserInterface : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject _visualization;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private CanvasGroup _group;

    private QuestScriptableObject _quest;

    public void Initialize(QuestScriptableObject quest)
    {
        _quest = quest;
        UpdateElementOpacity(quest);

        var title = quest.Title.SetSizeByPercentage(150);
        _text.text = $"{title}{Environment.NewLine}{Environment.NewLine}{quest.Description}";
    }

    private void UpdateElementOpacity(QuestScriptableObject quest)
    {
        _group.alpha = quest.Completed ? 0.5f : 1f;
    }

    private void Update()
    {
        UpdateElementOpacity(_quest);
    }

    public void SetElementVisibility(bool visible)
    {
        _visualization.SetActive(visible);
    }
}
