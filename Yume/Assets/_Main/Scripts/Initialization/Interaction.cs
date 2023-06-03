using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Interaction")]
public class Interaction : ScriptableObject
{
    private readonly List<UnityNode> _nodes = new();
}