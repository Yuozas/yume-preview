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
        SetAlpha(quest);

        _text.text = "<size=150%>" + quest.Title + "</size>"
            + Environment.NewLine + Environment.NewLine
            + quest.Description;
    }

    private void SetAlpha(QuestScriptableObject quest)
    {
        _group.alpha = quest.Completed ? 0.5f : 1f;
    }

    private void Update()
    {
        SetAlpha(_quest);
    }

    public void Set(bool active)
    {
        _visualization.SetActive(active);
    }
}
