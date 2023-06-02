using System.Collections.Generic;

public class TogglerGroup
{
    private readonly IEnumerable<IToggler> _toggleables;

    public TogglerGroup(IEnumerable<IToggler> toggleables)
    {
        foreach (var toggleable in toggleables)
            toggleable.OnEnabled += DeactivateExcept;
    }

    ~TogglerGroup()
    {
        foreach (var toggle in _toggleables)
            toggle.OnEnabled -= DeactivateExcept;
    }

    private void DeactivateExcept(IToggler exception)
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
