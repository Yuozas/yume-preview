using System.Collections.Generic;

public class Toggles
{
    private readonly IEnumerable<IToggle> _toggleables;

    public Toggles(IEnumerable<IToggle> toggleables)
    {
        foreach (var toggleable in toggleables)
            toggleable.OnEnabled += DeactivateExcept;
    }

    ~Toggles()
    {
        foreach (var toggle in _toggleables)
            toggle.OnEnabled -= DeactivateExcept;
    }

    private void DeactivateExcept(IToggle exception)
    {
        foreach (var toggle in _toggleables)
        {
            var equals = ReferenceEquals(toggle, exception);
            if (equals)
                continue;

            toggle.Disable();
        }
    }
}
