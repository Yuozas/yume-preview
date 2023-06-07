using System;

[Serializable]
public struct TypewriterSettings
{
    private const string SENTENCE = "This is a sentence. Welcome." + "\n" + "Please, make yourself at home!";
    public static readonly TypewriterSettings Default = new(SENTENCE, 0.025f);

    public string Sentence;
    public float Rate;

    public TypewriterSettings(string text, float rate)
    {
        Sentence = text;
        Rate = rate;
    }
}
