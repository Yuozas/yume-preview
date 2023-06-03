#if UNITY_EDITOR
using UnityEngine;
#endif

public class UnityNode
{
    public readonly INode Node;

#if UNITY_EDITOR
    public Vector3 Position;
#endif

    public UnityNode(INode node, Vector3 position)
    {
        Node = node;

#if UNITY_EDITOR
        SetPosition(position);
#endif
    }

#if UNITY_EDITOR
    public void SetPosition(Vector3 position)
    {
        Position = position;
    }
#endif
}
