using UnityEngine;

public static class SpriteExtensions
{
    public static byte[] ToBytes(this Sprite sprite)
    {
        return sprite.texture.EncodeToPNG();
    }

    public static Sprite ConvertByteArrayToSprite(this byte[] spriteBytes)
    {
        var texture =  spriteBytes.ConvertByteArrayToTexture2d();
        return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
    }

    public static Texture2D ConvertByteArrayToTexture2d(this byte[] spriteBytes)
    {
        var texture = new Texture2D(2, 2);
        texture.LoadImage(spriteBytes);
        return texture;
    }
}