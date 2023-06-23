using UnityEditor.Experimental.GraphView;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InteractionSearchWindow : ScriptableObject, ISearchWindowProvider
{
    private Action<string, SearchWindowContext> _onRequested;
    private Texture2D _texture;

    public void Initialize(Action<string, SearchWindowContext> onRequested)
    {
        _onRequested = onRequested;
        CreateContentTexture();
    }

    public List<SearchTreeEntry> CreateSearchTree(SearchWindowContext context)
    {
        const string Create = "Create";
        const string Basic = "Basic";
        const string Dialogue = "Dialogue";
        const string Audio = "Audio";

        return new List<SearchTreeEntry>()
        {
            CreateGroupEntry(Create, 0),
            CreateGroupEntry(Basic, 1),
            CreateEntry(INode.ENTRY, 2, _texture),
            CreateEntry(INode.EXIT, 2, _texture),
            CreateGroupEntry(Dialogue, 1),
            CreateEntry(INode.ENABLE, 2, _texture),
            CreateEntry(INode.DISABLE, 2, _texture),
            CreateEntry(INode.NAME, 2, _texture),
            CreateEntry(INode.PORTRAIT, 2, _texture),
            CreateEntry(INode.TYPEWRITER, 2, _texture),
            CreateGroupEntry(Audio, 1),
            CreateEntry(INode.MUSIC, 2, _texture),
            CreateEntry(INode.SFX, 2, _texture)
        };
    }

    public bool OnSelectEntry(SearchTreeEntry SearchTreeEntry, SearchWindowContext context)
    {
        var type = (string)SearchTreeEntry.userData;
        _onRequested?.Invoke(type, context);
        return true;
    }

    private SearchTreeEntry CreateEntry(string type, int level, Texture2D texture = null)
    {
        var content = new GUIContent(type, texture);
        return new SearchTreeEntry(content) { level = level, userData = type };
    }

    private SearchTreeEntry CreateGroupEntry(string type, int level, Texture2D texture = null)
    {
        var content = new GUIContent(type, texture);
        return new SearchTreeGroupEntry(content) { level = level };
    }

    private void CreateContentTexture()
    {
        _texture = new Texture2D(1, 1);
        _texture.SetPixel(0, 0, Color.clear);
        _texture.Apply();
    }
}