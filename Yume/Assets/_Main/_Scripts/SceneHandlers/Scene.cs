public readonly struct Scene
{
    public static readonly Scene MainMenuScene = "Main Menu";
    public static readonly Scene SettingsScene = "Settings";
    public static readonly Scene NewGameScene = "New Game";
    public static readonly Scene ContinueScene = "Continue";
    public static readonly Scene DemoScene = "Office_Demo";

    public string Name { get; }

    private Scene(string name)
    {
        Name = name;
    }

    public static implicit operator Scene(string sceneName) => new(sceneName);
}