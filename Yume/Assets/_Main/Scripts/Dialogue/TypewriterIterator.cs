using System.Text;

public class TypewriterIterator
{
    public string Current => _builder.ToString();

    private string _sentence;
    private readonly StringBuilder _builder;

    public TypewriterIterator()
    {
        _builder = new();
    }
    public void Set(string sentence)
    {
        _builder.Clear();
        _sentence = sentence;
    }

    public void Next()
    {
        var character = _sentence[_builder.Length];
        _builder.Append(character);
    }
}
