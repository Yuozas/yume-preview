using UnityEngine;
using System;

[Serializable]
public struct PortraitSettings
{
    public static readonly PortraitSettings Default = new(null, null);

    public Sprite Face;
    public Sprite Hair;

    public PortraitSettings(Sprite face, Sprite hair)
    {
        Face = face;
        Hair = hair;
    }
}
