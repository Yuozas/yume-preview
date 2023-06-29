using System.Linq;
using UnityEngine.SceneManagement;

public class SceneInteractionExecutor
{
    private readonly SceneInteractionScriptableObject[] _scriptableObjects;

    public SceneInteractionExecutor(SceneInteractionScriptableObject[] scriptableObjects)
    {
        _scriptableObjects = scriptableObjects;
        
        SceneManager.sceneLoaded += Execute;
    }

    ~SceneInteractionExecutor()
    {
        SceneManager.sceneLoaded -= Execute;
    }

    private void Execute(Scene scene, LoadSceneMode type)
    {
        var scriptableObject = _scriptableObjects.FirstOrDefault(scriptable => scriptable.SceneName == scene.name);
        if (scriptableObject is not null)
            scriptableObject.Interaction.Interact();
    }
}