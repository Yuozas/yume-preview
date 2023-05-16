using UnityEngine;
using TMPro;
using System.Collections;
using System;

public class DialogueBox : Singleton<DialogueBox>
{
    [Header("References")]
    [SerializeField] private GameObject _visualization;
    [SerializeField] private TMP_Text _text;

    public const float WAIT_IN_BETWEEN_LETTER = 0.05f;

    private bool _typing => _ienumerator is not null;
    private IEnumerator _ienumerator;
    private Action _onCompleted;
    private string _sentence;

    protected override void Awake()
    {
        base.Awake();
        //SetVisibility(false);
    }

    public void SetVisibility(bool visible)
    {
        _visualization.SetActive(visible);
    }

    public void Begin(string sentence, Action onCompleted)
    {
        _sentence = sentence;
        _onCompleted = onCompleted;

        Stop();

        _ienumerator = Typewriter(sentence);
        StartCoroutine(_ienumerator);
    }

    private void Stop()
    {
        if (_ienumerator is null) return;

        StopCoroutine(_ienumerator);
        _ienumerator = null;
    }

    IEnumerator Typewriter(string sentence)
    {
        _text.text = string.Empty;
        var rate = new WaitForSeconds(WAIT_IN_BETWEEN_LETTER);

        foreach (var letter in sentence)
        {
            _text.text += letter;
            yield return rate;
        }

        _onCompleted?.Invoke();
    }
}
