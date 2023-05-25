public struct DelayedExecutorSettings
{
    public static readonly DelayedExecutorSettings DEFAULT = new(0, 1);

    public int Cycles;
    public float Rate;

    public DelayedExecutorSettings(int cycles, float rate)
    {
        Cycles = cycles;
        Rate = rate;
    }
}