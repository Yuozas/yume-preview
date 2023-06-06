using System;

public interface IDrawable
{
    Action OnDrawn { get; }
    void Draw();
    void Set(GraphNode node);
}
