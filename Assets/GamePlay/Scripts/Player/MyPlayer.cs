using Assets.GamePlay.Scripts.Bonuses;
using Assets.GamePlay.Scripts.DemageToPlayerController;
using Assets.GamePlay.Scripts.Enemies;
using Assets.GamePlay.Scripts.GUI;
using Assets.GamePlay.Scripts.GUI.TowerCombinationPanel;
using Assets.GamePlay.Scripts.Map;
using Assets.GamePlay.Scripts.Mechanic;
using Assets.GamePlay.Scripts.storageTower;
using Assets.GamePlay.Scripts.Tower.Interfaces.BonusConveyor;
using Assets.GamePlay.Scripts.TowerClasses;
using Assets.GamePlay.Scripts.TowerClasses.TowerCombinations;
using Assets.Photon.MyScripts;
using Assets.scripts.serialization;
using Photon.Pun;
using PunTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using static MySceneManager;

namespace Assets.GamePlay.Scripts.Player
{
    public class MyPlayer : MonoBehaviour
    {
        [HideInInspector]
        public string playerName="defaultName";
        [HideInInspector]
        public string nickName = "player)";
        public GameObject onlinePlayer;

        public bool isItRealPlayer; //ці з'яўляецца гэты экзэмпляр гульцом гэтага кампутара
        public CombinationGenerator possibleCombinations;
        public TowerClasseGenerator towerClassCollection;
        public InputControl inputControl;
        public Builder builder;
        public BonusesBuilder bonusesBuilder;
        public GuiControl guiControl;
        public TowerCombinationPanel combinationPanel;
        public TimeController timeController;
        public BuildingsStorage buildingsStorage;
        public MapSerDeser mapSerDeser;
        public MapController mapController;
        public BlocksGenerator blocksGenerator;
        public Players players;
        public PhotonView photonView;
        public BonusConveyorManager bonusConveyorManager;
        public Level level;
        public FinanceControllAfterWaves financeControllAfterWaves;
        public OnlineConnector onlineConnector;
        public MySceneManager sceneManager;

        public virtual void Start()
        {

            if (!isItRealPlayer)
            {
                //калі гэты экзэмпляр прадстаўляе іншага гульца, то нічога не робім
                //possibleCombinations?.Initialize(this);
                //FindObjectOfType<Players>().UpdatePlayerForOtherPlayers();
                return;
            }
            towerClassCollection?.Initialize(this);
            possibleCombinations?.Initialize(this);
            inputControl?.Initialize(this);
            builder?.Initialize(this);
            bonusesBuilder?.Initialize(this);
            guiControl?.Initialize(this);
            combinationPanel?.Initialize(this);
            //timeController.Initialize();
            buildingsStorage?.Initialize(this);
            mapSerDeser?.Initialize(this);
            mapController?.Initialize(this);
            blocksGenerator?.Initialize(this);
            bonusConveyorManager?.Initialize(this);
            players?.Initialize(this);
            level?.Initialize(this);
            financeControllAfterWaves?.Initialize(this);

            EnemiesPull.Initialize();


            if (SceneManager.GetActiveScene().name == ESceneNames.PlayScene.ToString())
            {
                if (LevelManager.typeOfMap == LevelManager.ETypeOfLoadMap.hostLevel
                    || LevelManager.typeOfMap == LevelManager.ETypeOfLoadMap.clientLevel)
                {
                    MapForOnline.LoadSavedMap();
                    nickName = PhotonNetwork.LocalPlayer.NickName;
                    playerName = PhotonNetwork.LocalPlayer.UserId;
                }else
                {
                    level.StartMap();
                }
            } 
            else if (SceneManager.GetActiveScene().name == ESceneNames.LevelRedactorScene.ToString())
            {
                level.LoadCleanMap();

            }
            else if (SceneManager.GetActiveScene().name == ESceneNames.NewOnlineScene.ToString())
            {
                //level.LoadCleanMap();
                //onlineConnector?.Initialize(this);
                //onlineConnector.Connect();
                //playerName = LevelManager.nickName;
                if (LevelManager.typeOfMap == LevelManager.ETypeOfLoadMap.hostLevel)
                {
                    //onlineConnector.CreateRoom();
                } else
                {
                    //onlineConnector.JoinRoom();
                }
            }
            else
            {
                //mapSerDeser.LoadMapLevel();
            }
        }
        private void InitializeTowers()
        {
            var towers = GameObject.FindObjectsOfType<Tower.Tower>();
            foreach (var tower in towers)
            {
                tower.Owner = this;
                tower.Initialize();
            }
        }
        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                string json = JsonUtility.ToJson(this);
                json = JsonUtility.ToJson(possibleCombinations.possibleCombinations);
                var combSer = new TowerCombinationSer(possibleCombinations.possibleCombinations.ElementAt(1));
                json = JsonUtility.ToJson(combSer);
                TowerCombinationSer s = JsonUtility.FromJson<TowerCombinationSer>(json);
                json = JsonUtility.ToJson(combSer.effects.ElementAt(0));
            }
        }
    }
}
