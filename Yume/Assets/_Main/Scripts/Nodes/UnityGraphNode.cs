using UnityEngine;

public class UnityGraphNode
{
    public readonly INode Node;
    public Vector3 Position;

    public UnityGraphNode(INode node, Vector3 position)
    {
        Node = node;
        SetPosition(position);
    }

    public void SetPosition(Vector3 position)
    {
        Position = position;
    }
}
