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
            using var activeRealmSaveDetails = ServiceLocator.GetSingleton<IRealmActiveSaveHelper>().GetActiveSaveDetails();
            if(debuggingRealm.SaveDetails is not null && activeRealmSaveDetails.Result?.SaveId != debuggingRealm.SaveDetails?.SaveId)
            {
                var realmSaveManager = ServiceLocator.SingletonProvider.Get<IRealmActiveSaveHelper>();
                realmSaveManager.ChangeActiveSave(debuggingRealm.SaveDetails.SaveId);
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
