using System.Text;
using System;

[Serializable]
public class TypewriterIterator
{
    public string Current => _builder.ToString();

    private string _sentence;
    private StringBuilder _builder;

    public void Set(string sentence)
    {
        _builder = new StringBuilder();
        _sentence = sentence;
    }

    public void Next()
    {
        var character = _sentence[_builder.Length];
        _builder.Append(character);
    }
}
