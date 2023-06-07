using System.Text;
using System;

[Serializable]
public class TypewriterIterator
{
    public string Current => _builder.ToString();

    private string _sentence;
    private StringBuilder _builder;
    private bool _ignoreSpace;

    public void Set(string sentence, bool ignoreSpace)
    {
        _builder = new StringBuilder();
        _ignoreSpace = ignoreSpace;
        _sentence = sentence;
    }

    public void Next()
    {
        if (_ignoreSpace)
        {
            char letter;
            do
                letter = GetAndAppendLetter();
            while (char.IsWhiteSpace(letter) && _builder.Length < _sentence.Length);
        }
        else
            _ = GetAndAppendLetter();
    }

    public void Complete()
    {
        _builder.Clear();
        _builder.Append(_sentence);
    }

    private char GetAndAppendLetter()
    {
        var letter = _sentence[_builder.Length];
        _builder.Append(letter);
        return letter;
    }
}
