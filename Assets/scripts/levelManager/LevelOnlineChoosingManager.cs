using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class LevelOnlineChoosingManager : LevelChoosingManager
{
    public InputField roomNameText, nickNameText;
    public override void Start()
    {
        LevelManager.nameOfRoom = "";
        LevelManager.nickName = "";
        base.Start();
    }
    protected override void UpdateLevelsPanel()
    {
        base.UpdateLevelsPanel();
    }
    public override void OnLevelSelection(GameObject obj)
    {
        LevelManager.nameOfRoom = roomNameText.text;
        LevelManager.nickName = nickNameText.text;
        LevelManager.nameOfLevel = obj.transform.GetChild(0).GetComponent<Text>().text;
        sceneManager.LoadOnlineWaitingScene();
    }
    public void FindRoomWithAnyLevel()
    {
        LevelManager.nameOfRoom = roomNameText.text;
        LevelManager.nickName = nickNameText.text;
        LevelManager.nameOfLevel = "";
        LevelManager.typeOfMap = LevelManager.ETypeOfLoadMap.clientLevel;
        sceneManager.LoadOnlineWaitingScene();
    }
    public void HostLevel(GameObject obj)
    {
        LevelManager.typeOfMap = LevelManager.ETypeOfLoadMap.hostLevel;
        OnLevelSelection(obj);
    }
    public void JoinLevel(GameObject obj)
    {
        LevelManager.typeOfMap = LevelManager.ETypeOfLoadMap.clientLevel;
        OnLevelSelection(obj);
    }
    public void SetNickName(InputField text)
    {
        LevelManager.nickName = text.text;
    }
    public void SetRoomName(InputField text)
    {
        LevelManager.nameOfRoom = text.text;
    }
}
