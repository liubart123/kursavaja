using Assets.GamePlay.Scripts.Waves;
using Assets.scripts.serialization;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using static Assets.scripts.serialization.MapSerDeser;

//кляс для выбара лэвэлоў
public class LevelChoosingManager : MonoBehaviour
{
    public MySceneManager sceneManager;
    public GameObject level;
    public GameObject levelPanel;
    public JsonStorage JsonStorage;


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
        JsonStorage.GetNamesOfAllSavedLevels((levels)=> UpdateLevelsPanel(levels));
        
    }
    protected virtual void UpdateLevelsPanel(ICollection<string> levels) {
        foreach (var levelname in levels)
        {
            if (levelname == "")
                continue;
            string nameOfProgress = levelname + Level.nameOfSavingProgress;
            var temp = Instantiate(level, levelPanel.transform);
            temp.transform.GetChild(0).GetComponent<Text>().text = levelname;
            JsonStorage.GetJson(nameOfProgress,
                (waveManager)=> UpdateCountOfWavesAccordingToLevel(temp, 
                MapSerDeser.DeserializeWaveManager(waveManager)));
            
            temp.SetActive(true);
        }
    }
    //дабавіць да панэлі колькасць хвалей
    protected virtual void UpdateCountOfWavesAccordingToLevel(GameObject temp, WaveManagerSer waveManager)
    {
        if (waveManager != null)
        {
            temp.transform.GetChild(3).GetChild(1).GetComponent<Text>().text
                = waveManager.waveCounter.ToString();
        }
        else
        {
            temp.transform.GetChild(3).GetChild(1).GetComponent<Text>().text
                = "0";
        }
    }
    protected void DeleteChildren(GameObject obj)
    {
        for (int i = obj.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(obj.transform.GetChild(i).gameObject);
        }
    }
    public virtual void OnLevelSelection(GameObject obj)
    {
        LevelManager.nameOfLevel = obj.transform.GetChild(0).GetComponent<Text>().text;
        sceneManager.LoadPlayScene();
    }

    public virtual void ResetLevel(GameObject obj)
    {
        LevelManager.typeOfMap = LevelManager.ETypeOfLoadMap.newLevel;
        OnLevelSelection(obj);
    }
    public virtual void LoadLevel(GameObject obj)
    {
        LevelManager.typeOfMap = LevelManager.ETypeOfLoadMap.progress;
        OnLevelSelection(obj);
    }
}
