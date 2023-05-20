using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

public class OptionsNode : Node
{
    public void Draw()
    {
        var title = new Label("Options");
        titleContainer.Insert(0, title);
    }
}