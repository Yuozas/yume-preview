using SwiftLocator.Services.ServiceLocatorServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PreliminarySetupFinish : IPreliminarySetup
{
    public int Order => IPreliminarySetup.FINISH;

    public void Setup()
    {
        if(!Application.isPlaying)
            return;

        var sceneDataHandler = ServiceLocator.SingletonProvider.Get<SceneDataHandler>();
        SceneManager.LoadScene(sceneDataHandler.MainMenuSceneName, LoadSceneMode.Single);
    }
}
