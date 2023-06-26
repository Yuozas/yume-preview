using UnityEngine;

public static class SpriteExtensions
{
    public static byte[] ToBytes(this Sprite sprite)
    {
        return sprite.texture.EncodeToPNG();
    }
}