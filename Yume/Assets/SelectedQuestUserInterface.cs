using UnityEngine;
using TMPro;
using System;

public class SelectedQuestUserInterface : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject _visualization;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private CanvasGroup _group;

    public void Initialize(QuestScriptableObject quest)
    {
        _group.alpha = quest.Completed ? 0.5f : 1f;
        _text.text = "<size=150%>" + quest.Title + "</size>" 
            + Environment.NewLine + Environment.NewLine
            + quest.Description;
    }

    public void Set(bool active)
    {
        _visualization.SetActive(active);
    }
}
