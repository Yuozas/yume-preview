using UnityEngine;

public struct PortraitHandlerSettings
{
    public static readonly PortraitHandlerSettings DEFAULT = new(null, null);

    public Sprite Face;
    public Sprite Hair;

    public PortraitHandlerSettings(Sprite face, Sprite hair)
    {
        Face = face;
        Hair = hair;
    }
}
