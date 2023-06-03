using UnityEngine;

public class UnityNode
{
    public readonly INode Node;
    public Vector3 Position;

    public UnityNode(INode node, Vector3 position)
    {
        Node = node;
        SetPosition(position);
    }

    public void SetPosition(Vector3 position)
    {
        Position = position;
    }
}
