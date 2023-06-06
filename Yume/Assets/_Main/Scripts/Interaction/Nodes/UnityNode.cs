#if UNITY_EDITOR
using UnityEngine;
#endif
using System;

[Serializable]
public class UnityNode
{
    [field: SerializeReference] public INode Node { get; private set; }
    public string Type => Node.Type;

#if UNITY_EDITOR
    public Vector2 Position;
    private Action _onUpdated;
#endif

    public UnityNode(INode node, Vector2 position)
    {
        Node = node;

#if UNITY_EDITOR
        SetPosition(position);
#endif
    }

#if UNITY_EDITOR
    public void Set(Action onUpdated)
    {
        _onUpdated = onUpdated;
    }
#endif

#if UNITY_EDITOR
    public void SetPosition(Vector2 position)
    {
        Position = position;
        _onUpdated?.Invoke();
    }
#endif
}
