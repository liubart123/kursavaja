using Assets.scripts.serialization;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using static Assets.scripts.serialization.MapSerDeser;

//кляс для выбара лэвэлоў
public class LevelOnlineChoosingManager : LevelChoosingManager
{
    public override void OnLevelSelection(GameObject obj)
    {
        LevelManager.nameOfLevel = obj.transform.GetChild(0).GetComponent<Text>().text;
        sceneManager.LoadPlayScene();
    }

    public override void ResetLevel(GameObject obj)
    {
        LevelManager.typeOfMap = LevelManager.ETypeOfLoadMap.newLevelOnline;
        OnLevelSelection(obj);
    }
    public override void LoadLevel(GameObject obj)
    {
        LevelManager.typeOfMap = LevelManager.ETypeOfLoadMap.progressOnline;
        OnLevelSelection(obj);
    }
}
