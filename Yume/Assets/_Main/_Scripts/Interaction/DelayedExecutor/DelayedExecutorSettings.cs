using System;

[Serializable]
public struct DelayedExecutorSettings
{
    public static readonly DelayedExecutorSettings Default = new(1, 0.05f);

    public int Cycles;
    public float Rate;

    public DelayedExecutorSettings(int cycles, float rate)
    {
        Cycles = cycles;
        Rate = rate;
    }
}