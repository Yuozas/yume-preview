using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

public class OptionsNode : UnityEditor.Experimental.GraphView.Node
{
    public void Draw()
    {
        var title = new Label("Options");
        titleContainer.Insert(0, title);
    }
}