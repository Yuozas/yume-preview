using UnityEditor;

public static class UnityEditorPreliminarySetup
{
    [MenuItem("Tools/Preliminary setup")]
    private static void Setup()
    {
        PreliminarySetup.Setup();
    }
}