using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//кляз з статычнымі зменнымі для правільнай загрузкі ўзроўняў, пры пераходзе з меню да гульні
public class LevelManager 
{
    public const string defaultNameOfLevel = "empty";
    public static string nameOfLevel = defaultNameOfLevel;
    public static ETypeOfLoadMap typeOfMap;
    public static string nameOfRoom { get; set; }
    public static string nickName { get; set; }
    public enum ETypeOfLoadMap
    {
        newLevel,
        progress,
        newLevelOnline,
        progressOnline,
        hostLevel,  //загружаецца ўзровень для анлайна і чакае гульцоў
        clientLevel //можа паключацца да іншых анлайн узроўняў

    }
}
