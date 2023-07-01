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
        const string Decision = "Decision";
        const string Transitions = "Transition To Destination";
        const string SliderGame = "Slider Game";
        return new List<SearchTreeEntry>()
        {
            CreateGroupEntry(Create, 0),
            CreateGroupEntry(Basic),
            CreateEntry(INode.ENTRY),
            CreateEntry(INode.EXIT),
            CreateGroupEntry(Dialogue),
            CreateEntry(INode.ENABLE),
            CreateEntry(INode.DISABLE),
            CreateEntry(INode.NAME),
            CreateEntry(INode.PORTRAIT),
            CreateEntry(INode.TYPEWRITER),
            CreateGroupEntry(Audio),
            CreateEntry(INode.MUSIC),
            CreateEntry(INode.PLAY_SOUND_EFFECT),
            CreateGroupEntry(Decision),
            CreateEntry(INode.ENABLE_DECISIONS),
            CreateEntry(INode.DISABLE_DECISIONS),
            CreateEntry(INode.SET_DECISION_CHOICES),
            CreateGroupEntry(Transitions),
            CreateEntry(INode.TRANSITION_TO_DESTINATION),
            CreateGroupEntry(SliderGame),
            CreateEntry(INode.ENABLE_SLIDER_GAME),
            CreateEntry(INode.DISABLE_SLIDER_GAME),
            CreateEntry(INode.PLAY_SLIDER_GAME),
        };
    }

    public bool OnSelectEntry(SearchTreeEntry SearchTreeEntry, SearchWindowContext context)
    {
        var type = (string)SearchTreeEntry.userData;
        _onRequested?.Invoke(type, context);
        return true;
    }

    private SearchTreeEntry CreateEntry(string type)
    {
        var content = new GUIContent(type, _texture);
        return new SearchTreeEntry(content) { level = 2, userData = type };
    }

    private SearchTreeEntry CreateGroupEntry(string type, int level = 1)
    {
        var content = new GUIContent(type);
        return new SearchTreeGroupEntry(content) { level = level };
    }

    private void CreateContentTexture()
    {
        _texture = new Texture2D(1, 1);
        _texture.SetPixel(0, 0, Color.clear);
        _texture.Apply();
    }
}