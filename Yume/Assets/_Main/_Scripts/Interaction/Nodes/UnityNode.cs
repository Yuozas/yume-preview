using UnityEngine;
using System;

[Serializable]
public class UnityNode
{
    [field: SerializeReference] public INode Node { get; private set; }
    public string Type => Node.Type;

#if UNITY_EDITOR
    public Vector2 Position;
#endif

    public UnityNode(INode node, Vector2 position)
    {
        Node = node;

#if UNITY_EDITOR
        SetPosition(position);
#endif
    }

#if UNITY_EDITOR
    public void SetPosition(Vector2 position)
    {
        Position = position;
    }
#endif
}
