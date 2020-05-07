using Assets.GamePlay.Scripts.Building;
using Assets.GamePlay.Scripts.Enemies;
using Assets.GamePlay.Scripts.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    MyPlayer owner;
    public static string pathBeforeSaving = "";
    public static string nameOfSavingProgress = "Progress";
    public static string nameOfSavingLevel = "Level";
    public static string nameOfFileType = ".txt";
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
    public void Initialize(MyPlayer pl)
    {
        owner = pl;
       
    }
    //загрузіць мапу ў адпаведнасці з выбарам гульца
    public void StartMap()
    {
        if (LevelManager.typeOfMap == LevelManager.ETypeOfLoadMap.newLevel)
        {
            LoadNewLevel();
        }
        else if (LevelManager.typeOfMap == LevelManager.ETypeOfLoadMap.progress)
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
        owner.mapSerDeser.LoadMapLevel(pathBeforeSaving + LevelManager.nameOfLevel + nameOfSavingLevel + nameOfFileType);
    }
    public void LoadProgress()
    {
        owner.mapSerDeser.LoadMap(pathBeforeSaving + LevelManager.nameOfLevel + nameOfSavingProgress + nameOfFileType);
    }
    public void SaveLevel()
    {
        owner.mapSerDeser.SaveMapLevel(pathBeforeSaving + LevelManager.nameOfLevel + nameOfSavingLevel + nameOfFileType);
        SaveProgress();
    }
    public void SaveProgress()
    {
        owner.mapSerDeser.SaveMap(pathBeforeSaving + LevelManager.nameOfLevel + nameOfSavingProgress + nameOfFileType);

    }
}
