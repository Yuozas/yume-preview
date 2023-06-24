using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;

[CreateAssetMenu(menuName = "Interaction")]
public class Interaction : ScriptableObject
{
    [field: SerializeReference] public List<UnityNode> UnityNodes { get; private set; } = new();

#if UNITY_EDITOR
    public void Clear()
    {
        UnityNodes.Clear();
        Save();
    }

    [ContextMenu("Fix")]
    public void Fix()
    {
        var nodes = UnityNodes
            .Select(unityNode => unityNode.Node)
            .Cast<CompositeNode>()
            .ToList();

        foreach (var item in nodes)
            item.AddDefaultConnection();
    }

    public void Interact()
    {
        UnityNodes.First(node => node.Type is INode.ENTRY).Node.Execute();
    }

    public bool Contains(string type)
    {
        return UnityNodes.Any(node => node.Type == type);
    }

    public void Add(UnityNode node)
    {
        var contains = Contains(node);
        if (!contains)
        {
            UnityNodes.Add(node);
            Save();
        }
    }

    public void Remove(UnityNode node)
    {
        var contains = Contains(node);
        if (contains)
        {
            UnityNodes.Remove(node);
            Save();
        }
    }

    private bool Contains(UnityNode node)
    {
        return UnityNodes.Contains(node);
    }

    [ContextMenu("Save")]
    public void Save()
    {
        EditorUtility.SetDirty(this);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
#endif
}