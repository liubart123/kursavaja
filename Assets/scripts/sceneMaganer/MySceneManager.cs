using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneManager : MonoBehaviour
{
    public enum ESceneNames {
        LevelRedactorScene,
        MainMenu,
        PlayScene, 
        OnlinePlayScene,
        LevelChoosingScene,
        NewOnlineScene,
        LevelOnlineChoosingScene,
        OnlineWaitingScene
    }

    public void LoadLevelRedactorScene()
    {
        SceneManager.LoadScene(ESceneNames.LevelRedactorScene.ToString());
    }
    public void LoadMainMenuScene()
    {
        SceneManager.LoadScene(ESceneNames.MainMenu.ToString());
    }
    public void LoadPlayScene()
    {
        SceneManager.LoadScene(ESceneNames.PlayScene.ToString());
    }
    public void LoadOnlineScene()
    {
        SceneManager.LoadScene(ESceneNames.OnlinePlayScene.ToString());
    }
    public void LoadLevelChoosingScene()
    {
        SceneManager.LoadScene(ESceneNames.LevelChoosingScene.ToString());
    }
    public void LoadNewOnlineScene()
    {
        SceneManager.LoadScene(ESceneNames.NewOnlineScene.ToString());
    }
    
    public void LoadLevelOnlineChoosingScene()
    {
        SceneManager.LoadScene(ESceneNames.LevelOnlineChoosingScene.ToString());
    }
    public void LoadOnlineWaitingScene()
    {
        SceneManager.LoadScene(ESceneNames.OnlineWaitingScene.ToString());
    }
}
