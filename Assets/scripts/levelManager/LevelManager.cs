using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager 
{
    public static string nameOfLevel = "save";
    public static ETypeOfLoadMap typeOfMap;
    public enum ETypeOfLoadMap
    {
        newLevel,
        progress
    }
}
