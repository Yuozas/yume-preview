using SwiftLocator.Services.ServiceLocatorServices;
using UnityEngine.SceneManagement;

public class PreliminarySetupFinish : IPreliminarySetup
{
    public int Order => IPreliminarySetup.FINISH;

    public void Setup()
    {
        var sceneDataHandler = ServiceLocator.SingletonProvider.Get<SceneDataHandler>();
        SceneManager.LoadScene(sceneDataHandler.MainMenuSceneName, LoadSceneMode.Single);
    }
}
