using UnityEditor;
using System;
using UnityEngine;

public static class SpriteExtensions
{
#if UNITY_EDITOR
    public static string GetResourcesPath(this Sprite sprite)
    {
        const string searchString = "Resources/";

        var path = AssetDatabase.GetAssetPath(sprite);

        var startIndex = path.LastIndexOf(searchString) switch
        {
            var index when index >= 0 => index + searchString.Length,
            _ => throw new Exception("The string path does not contain the 'Resources/' segment.")
        };

        var result = path[startIndex..];

        // Remove the file extension
        var lastDotIndex = result.LastIndexOf('.');
        if (lastDotIndex >= 0)
            result = result[..lastDotIndex];

        return result;
    }
#endif
}