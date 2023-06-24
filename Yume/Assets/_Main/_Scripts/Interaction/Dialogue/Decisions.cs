public class Decisions : ITogglerProvider
{
    public IToggler Toggler { get; private set; }
    public readonly ChoiceGroup Choices;

    public Decisions()
    {
        Toggler = new Toggler();
        Choices = new ChoiceGroup();
    }
}