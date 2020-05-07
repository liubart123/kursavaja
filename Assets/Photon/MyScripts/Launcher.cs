using Assets;
using Assets.GamePlay.Scripts.Player;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PunTesting
{
    public class Launcher : MonoBehaviourPunCallbacks
    {
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
        public void Connect(Text text = null)
        {
            if (text == null || text.text == "")
            {
                PhotonNetwork.NickName = "player_" + Random.Range(0, 20000).ToString();
                owner.playerName = PhotonNetwork.NickName;
            }
            else
            {
                PhotonNetwork.NickName = text.text;
                owner.playerName = text.text;
            }

            // we check if we are connected or not, we join if we are , else we initiate the connection to the server.
            if (OnlineManager.DoNotOwnCalculations)
            {
                // #Critical we need at this point to attempt joining a Random Room. If it fails, we'll get notified in OnJoinRandomFailed() and we'll create one.
                PhotonNetwork.JoinRandomRoom();
            }
            else
            {
                // #Critical, we must first and foremost connect to Photon Online Server.
                PhotonNetwork.ConnectUsingSettings();
                PhotonNetwork.GameVersion = gameVersion;
            }
            OnlineManager.CreateNetworkObjects = true;
        }


        #endregion

        public override void OnConnectedToMaster()
        {
            PhotonNetwork.JoinRandomRoom();
        }


        public override void OnDisconnected(DisconnectCause cause)
        {
            owner.players.RemovePlayerForOtherPlayers();
        }
        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            PhotonNetwork.CreateRoom(null, new RoomOptions());
        }

        public override void OnJoinedRoom()
        {
            PhotonNetwork.LoadLevel(MySceneManager.ESceneNames.OnlinePlayScene.ToString());
            Debug.Log("we entered the room");
            owner.players.AddPlayerForOthersPlayers();
        }
    }
}
