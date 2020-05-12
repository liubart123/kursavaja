using Assets.GamePlay.Scripts.Building;
using Assets.GamePlay.Scripts.Enemies;
using Assets.GamePlay.Scripts.Player;
using Assets.scripts.serialization;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static LevelManager;

public class Level : MonoBehaviour
{
    MyPlayer owner;
    public static string nameOfSavingProgress = "Progress";
    public static string nameOfSavingLevel = "Level";
    private string nameOfSave;
    public string NameOfSave {
        get
        {
            return nameOfSave;
        } 
        set {
            nameOfSave = value;
            LevelManager.nameOfLevel = value;
        } 
    }   //імя сэйва, калі знаходзіцца ў рэдактары ўзроўняў

    public JsonStorage jsonStorage;
    public JsonStorage jsonRemoteStorage;
    private static JsonStorage jsonStorageStatic;
    public void Initialize(MyPlayer pl)
    {
        owner = pl;
        //выбар кляса для кантроля файлаў захавання
        //ці лакальнае схоівшча, ці ўадаленае
        if (jsonStorage !=null && jsonRemoteStorage != null)
        {
            if (LevelManager.typeOfMap == ETypeOfLoadMap.progress ||
                LevelManager.typeOfMap == ETypeOfLoadMap.newLevel)
            {
                jsonStorageStatic = jsonStorage;
            }
            else
            {
                jsonStorageStatic = jsonRemoteStorage;
            }
        } else if (jsonStorage != null) { jsonStorageStatic = jsonStorage; }
        else if (jsonRemoteStorage != null) { jsonStorageStatic = jsonRemoteStorage; }
    }
    //загрузіць мапу ў адпаведнасці з выбарам гульца(для кааператыву)
    public void StartMap()
    {
        if (typeOfMap == ETypeOfLoadMap.newLevel ||
            typeOfMap == ETypeOfLoadMap.newLevelOnline)
        {
            LoadNewLevel();
        }
        else if (typeOfMap == ETypeOfLoadMap.progress ||
            typeOfMap == ETypeOfLoadMap.progressOnline)
        {
            LoadProgress();
        }
    }
    public void LoadCleanMap()
    {
        ICollection<Enemy> enemies = FindObjectsOfType<Enemy>();
        foreach (var e in enemies)
        {
            e.Die();
        }
        ICollection<Building> buildings = FindObjectsOfType<Building>();
        foreach (var b in buildings)
        {
            b.Die();
        }

    }
    public void LoadNewLevel()
    {
        if (typeOfMap == ETypeOfLoadMap.hostLevel || typeOfMap == ETypeOfLoadMap.clientLevel)
            return;
        jsonStorageStatic.GetJson(LevelManager.nameOfLevel + nameOfSavingLevel,
            (json) => owner.mapSerDeser.DeserializeMapFromJson(json));
        //owner.mapSerDeser.LoadMapLevel(pathBeforeSaving + LevelManager.nameOfLevel + nameOfSavingLevel + nameOfFileType);
    }
    public void LoadProgress()
    {
        if (typeOfMap == ETypeOfLoadMap.hostLevel || typeOfMap == ETypeOfLoadMap.clientLevel)
            return;
        jsonStorageStatic.GetJson(LevelManager.nameOfLevel + nameOfSavingProgress,
            (json)=> owner.mapSerDeser.DeserializeMapFromJson(json));
        
        //owner.mapSerDeser.LoadMap(pathBeforeSaving + LevelManager.nameOfLevel + nameOfSavingProgress + nameOfFileType);
    }
    public void SaveLevel()
    {
        if (typeOfMap == ETypeOfLoadMap.hostLevel || typeOfMap == ETypeOfLoadMap.clientLevel)
            return;
        string json = owner.mapSerDeser.SerializeMapToJson();
        jsonStorageStatic.SaveJson(json, LevelManager.nameOfLevel + nameOfSavingLevel);
        //owner.mapSerDeser.SaveMapLevel(pathBeforeSaving + LevelManager.nameOfLevel + nameOfSavingLevel + nameOfFileType);
        SaveProgress();
    }
    public void SaveProgress()
    {
        if (typeOfMap == ETypeOfLoadMap.hostLevel || typeOfMap == ETypeOfLoadMap.clientLevel)
            return;
        string json = owner.mapSerDeser.SerializeMapToJson();
        jsonStorageStatic.SaveJson(json, LevelManager.nameOfLevel + nameOfSavingProgress);
        //owner.mapSerDeser.SaveMap(pathBeforeSaving + LevelManager.nameOfLevel + nameOfSavingProgress + nameOfFileType);
    }
    public GameObject gameOverPanel;
    public void GameOver()
    {
        owner.guiControl.CloseAllPanels();
        gameOverPanel.SetActive(true);
    }
}
