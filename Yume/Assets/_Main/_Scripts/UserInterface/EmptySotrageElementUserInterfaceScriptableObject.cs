using UnityEngine;
// Todo. Refactor, this should not be a ScriptableObject.
[CreateAssetMenu(menuName = "InGameMenu/Empty")]
public class EmptySotrageElementUserInterfaceScriptableObject : StorageElementUserInterfaceScriptableObject
{
    protected override bool MultipleContents => false;

    protected override void SetupMenuElement()
    {
    }
}
