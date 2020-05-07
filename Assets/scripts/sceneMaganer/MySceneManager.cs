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
        LevelChoosingScene
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
}
