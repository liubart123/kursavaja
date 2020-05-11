using Assets;
using Assets.GamePlay.Scripts.Player;
using Assets.Photon.MyScripts;
using Assets.scripts.serialization;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using static Assets.scripts.serialization.MapSerDeser;

namespace PunTesting
{

    //кляс для стварэння злучэння з гульцамі
    public class OnlineConnector : MonoBehaviourPunCallbacks
    {
        public Text connectStatus, roomStatus, playersStatus, status;
        public GameObject statusPanel;
        public PhotonView photonView;
        public MapSerDeser mapSerDeser;

        #region Private Serializable Fields


        #endregion


        #region Private Fields


        /// <summary>
        /// This client's version number. Users are separated from each other by gameVersion (which allows you to make breaking changes).
        /// </summary>
        string gameVersion = "1";


        #endregion


        #region MonoBehaviour CallBacks


        /// <summary>
        /// MonoBehaviour method called on GameObject by Unity during early initialization phase.
        /// </summary>
        void Awake()
        {
            // #Critical
            // this makes sure we can use PhotonNetwork.LoadLevel() on the master client and all clients in the same room sync their level automatically
            PhotonNetwork.AutomaticallySyncScene = true;
            Connect();
        }


        /// <summary>
        /// MonoBehaviour method called on GameObject by Unity during initialization phase.
        /// </summary>
        void Start()
        {
            //Connect();
        }


        #endregion



        #region Public Methods
        private Assets.GamePlay.Scripts.Player.MyPlayer owner;

        public void Initialize(Assets.GamePlay.Scripts.Player.MyPlayer pl)
        {
            owner = pl;
        }
        /// <summary>
        /// Start the connection process.
        /// - If already connected, we attempt joining a random room
        /// - if not yet connected, Connect this application instance to Photon Cloud Network
        /// </summary>
        public void Connect(string nickName = null)
        {
            if (PhotonNetwork.IsConnected)
                PhotonNetwork.Disconnect();
            if (nickName == null || nickName == "")
            {
                PhotonNetwork.NickName = "player_" + Random.Range(0, 20000).ToString();
                //owner.playerName = PhotonNetwork.NickName;
            }
            else
            {
                PhotonNetwork.NickName = nickName;
            }

            status.text = "connecting...";
            PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.GameVersion = gameVersion;
        }
        public void Disconnect()
        {
            if (PhotonNetwork.IsConnected)
                PhotonNetwork.Disconnect();
            //FindObjectOfType<MySceneManager>().LoadMainMenuScene();
        }


        #endregion

        public override void OnConnectedToMaster()
        {
            //PhotonNetwork.JoinRandomRoom();
            connectStatus.text = "connected to server!";
            //Debug.Log("we connected to the server");
            if (LevelManager.typeOfMap == LevelManager.ETypeOfLoadMap.clientLevel)
            {
                status.text = "joining room...";
                JoinRoom();
            }
            else if (LevelManager.typeOfMap == LevelManager.ETypeOfLoadMap.hostLevel) {
                status.text = "creating room...";
                CreateRoom();
            }
        }


        public override void OnDisconnected(DisconnectCause cause)
        {
            //owner.players.RemovePlayerForOtherPlayers();
            //connectStatus.text = "no connection";
            //owner.sceneManager.LoadLevelOnlineChoosingScene();
        }
        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            roomStatus.text = "room finding failed";
            //PhotonNetwork.CreateRoom(null, new RoomOptions());

        }

        public override void OnJoinedRoom()
        {
            roomStatus.text = "room was found: " + PhotonNetwork.CurrentRoom.Name;
            UpdatePlayersCount();
            //PhotonNetwork.LoadLevel(MySceneManager.ESceneNames.OnlinePlayScene.ToString());
            //Debug.Log("we entered the room");
            //owner.players.AddPlayerForOthersPlayers();
        }

        public void CreateRoom()
        {
            string roomName = LevelManager.nameOfRoom;
            if (roomName == null || roomName == "")
            {
                roomName = Random.Range(0, 1234556f).ToString();
            }
            RoomOptions roomOptions = new RoomOptions();
            roomOptions.CustomRoomPropertiesForLobby = new string[]{ "levelName" };
            roomOptions.CustomRoomProperties = new ExitGames.Client.Photon.Hashtable() { { "levelName", LevelManager.nameOfLevel } };
            PhotonNetwork.CreateRoom(roomName, roomOptions);
        }
        public void JoinRoom()
        {
            string roomName = LevelManager.nameOfRoom;

            if (roomName == null || roomName == "")
            {
                ExitGames.Client.Photon.Hashtable expectedCustomRoomProperties = new ExitGames.Client.Photon.Hashtable() { { "levelName", LevelManager.nameOfLevel } };
                if (LevelManager.nameOfLevel=="")
                    PhotonNetwork.JoinRandomRoom();
                else
                {
                    PhotonNetwork.JoinRandomRoom(expectedCustomRoomProperties, 0);
                    //var rooms = PhotonNetwork.CurrentRoom.
                }
            } else
            {
                PhotonNetwork.JoinRoom(roomName);
            }
        }

        public override void OnPlayerEnteredRoom(Player other)
        {
            UpdatePlayersCount();
            status.text = other.NickName + "joined room";
            Debug.Log(other.NickName + "join room");
            //Debug.LogFormat("OnPlayerEnteredRoom() {0}", other.NickName); // not seen if you're the player connecting


            if (PhotonNetwork.IsMasterClient)
            {

            }
        }
        private void Update()
        {
            UpdatePlayersCount();
        }
        public override void OnPlayerLeftRoom(Player other)
        {
            if (other.IsMasterClient)
            {
                Disconnect();
            }
            UpdatePlayersCount();
            status.text = other.NickName + "left room";
            Debug.Log(other.NickName + " left room");
            //Debug.LogFormat("OnPlayerLeftRoom() {0}", other.NickName); // seen when other disconnects


            if (PhotonNetwork.IsMasterClient)
            {
                //Debug.LogFormat("OnPlayerLeftRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient); // called before OnPlayerLeftRoom

                //LeaveRoom();
                //LoadArena();
            }
        }
        private void UpdatePlayersCount()
        {
            if (PhotonNetwork.CurrentRoom!=null)
                playersStatus.text = "room players count: " + PhotonNetwork.CurrentRoom.PlayerCount;
            //foreach (var pl in PhotonNetwork.CurrentRoom.Players)
            //{
            //    //playersStatus.text+="\n"+pl.
            //}
        }

        public JsonStorage jsonStorage;

        //пачаць гульню, зачыніць пакой
        public void StartLevel()
        {
            if (PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.CurrentRoom.IsOpen = false;
                jsonStorage.GetJson(LevelManager.nameOfLevel + Level.nameOfSavingProgress,
                    (json) => photonView.RPC("LoadLevelFromOtherPlayer", RpcTarget.All, json));
            }
        }

        [PunRPC]
        private void LoadLevelFromOtherPlayer(string json)
        {
            MapForOnline.jsonOfMapToLoad = json;
            PhotonNetwork.LoadLevel(MySceneManager.ESceneNames.PlayScene.ToString());
            //owner.mapSerDeser.DeserializeMapFromJson(json);
            //statusPanel.SetActive(false);
        }
    }
}
