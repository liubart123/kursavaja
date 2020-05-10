using Assets.scripts.serialization;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

//кляс для выбара лэвэлоў
public class LevelChoosingManager : MonoBehaviour
{
    public MySceneManager sceneManager;
    public GameObject level;
    public GameObject levelPanel;
    public virtual void Start()
    {
        UpdateLevelsPanel();
        LevelManager.nameOfLevel = LevelManager.defaultNameOfLevel;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected virtual void UpdateLevelsPanel()
    {
        DeleteChildren(levelPanel);
        FileInfo fi = new FileInfo(Application.persistentDataPath + Level.pathBeforeSaving);
        DirectoryInfo di = new DirectoryInfo(Application.persistentDataPath + Level.pathBeforeSaving);
        var files = di.GetFiles();
        foreach(var file in files)
        {
            if (file.Name.Contains(Level.nameOfSavingLevel) &&
                //file.Name.Contains(Level.nameOfFileType) &&
                file.Extension == Level.nameOfFileType)
            {
                string levelname = file.Name;
                int pos = levelname.IndexOf(Level.nameOfSavingLevel);
                levelname = levelname.Substring(0,pos);
                var temp = Instantiate(level, levelPanel.transform);
                temp.transform.GetChild(0).GetComponent<Text>().text = levelname;
                string nameOfProgress = file.Name.Substring(0, file.Name.IndexOf(Level.nameOfSavingLevel));
                var waveManager = MapSerDeser.DeserializeWaveManager(nameOfProgress + Level.nameOfSavingProgress + Level.nameOfFileType);
                if (waveManager != null)
                {
                    temp.transform.GetChild(3).GetChild(1).GetComponent<Text>().text
                        = waveManager.waveCounter.ToString();
                }
                temp.SetActive(true);
            }
        }
    }
    protected void DeleteChildren(GameObject obj)
    {
        for (int i = obj.transform.childCount-1; i >= 0; i--)
        {
            Destroy(obj.transform.GetChild(i).gameObject);
        }
    }
    public virtual void OnLevelSelection(GameObject obj)
    {
        LevelManager.nameOfLevel = obj.transform.GetChild(0).GetComponent<Text>().text;
        sceneManager.LoadPlayScene();
    }

    public void ResetLevel(GameObject obj)
    {
        LevelManager.typeOfMap = LevelManager.ETypeOfLoadMap.newLevel;
        OnLevelSelection(obj);
    }
    public void LoadLevel(GameObject obj)
    {
        LevelManager.typeOfMap = LevelManager.ETypeOfLoadMap.progress;
        OnLevelSelection(obj);
    }
}
