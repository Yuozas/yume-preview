using UnityEngine;
using System;
using UnityEditor;

public static class SpriteExtensions
{
    public static string GetResourcesPath(this Sprite sprite)
    {
        const string searchString = "Resources/";

        var path = AssetDatabase.GetAssetPath(sprite);

        var startIndex = path.LastIndexOf(searchString) switch
        {
            var index when index >= 0 => index + searchString.Length,
            _ => throw new Exception("The string path does not contain the 'Resources/' segment.")
        };

        return path[startIndex..];
    }
}
