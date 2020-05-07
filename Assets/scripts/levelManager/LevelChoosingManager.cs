using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class LevelChoosingManager : MonoBehaviour
{
    public MySceneManager sceneManager;
    public GameObject level;
    public GameObject levelPanel;
    void Start()
    {
        UpdateLevelsPanel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void UpdateLevelsPanel()
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
                temp.SetActive(true);
            }
        }
    }
    private void DeleteChildren(GameObject obj)
    {
        for (int i = obj.transform.childCount-1; i >= 0; i--)
        {
            Destroy(obj.transform.GetChild(i).gameObject);
        }
    }
    public void OnLevelSelection(GameObject obj)
    {
        LevelManager.nameOfLevel = obj.transform.GetChild(0).GetComponent<Text>().text;
        sceneManager.LoadPlayScene();
    }
}
