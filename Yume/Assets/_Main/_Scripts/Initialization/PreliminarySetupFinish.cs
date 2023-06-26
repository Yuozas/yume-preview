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

        using var realm = ServiceLocator.SingletonProvider.Get<IRealmContext>().GetGlobalRealm();
        if(realm.TryGet<DebuggingRealm>(out var debuggingRealm))
        {
            if(debuggingRealm.SaveId is not 0)
            {
                var realmSaveManager = ServiceLocator.SingletonProvider.Get<IRealmActiveSaveHelper>();
                realmSaveManager.ChangeActiveSave(debuggingRealm.SaveId);
            }
            if(debuggingRealm.SceneName != SceneManager.GetActiveScene().name)
            {
                SceneManager.LoadScene(debuggingRealm.SceneName, LoadSceneMode.Single);
                return;
            }
        }

        SceneManager.LoadScene(Scene.MainMenuScene.Name, LoadSceneMode.Single);
    }
}
