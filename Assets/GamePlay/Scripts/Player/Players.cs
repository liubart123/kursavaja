using Assets.GamePlay.Scripts.Map;
using Assets.GamePlay.Scripts.TowerClasses.TowerCombinations;
using Photon.Pun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.GamePlay.Scripts.Player
{
    //кляс адказны, каб астатнія гульцы былі ў курсе аб стане гэтага гульца
    public class Players : MonoBehaviour
    {
        public MyPlayer owner;
        public GameObject otherPlayerObj;   //аб'ект, які ствараецца каб захоўваць MyPLayer іншага гульца
        public List<MyPlayer> players = new List<MyPlayer>();
        PhotonView photonView;
        public void Initialize(MyPlayer pl)
        {
            owner = pl;
            photonView = GetComponent<PhotonView>();
        }
        public MyPlayer GetPlayerByName(string name)
        {
            if (!players.Any(el => el.playerName == name))
            {
                AddPlayer(name);
            }
            return players.FirstOrDefault(el => el.playerName.Equals(name));
        }
        [PunRPC]
        private void AddPlayer(string name)
        {
            GameObject temp = Instantiate(otherPlayerObj, transform);
            players.Add(temp.GetComponent<MyPlayer>());
            temp.GetComponent<MyPlayer>().playerName = name;
            temp.GetComponent<MyPlayer>().possibleCombinations?.Initialize(temp.GetComponent<MyPlayer>());
            //UpdatePlayerForOtherPlayers();
        }
        [PunRPC]
        private void RemovePlayer(string name)
        {
            players.RemoveAll(el => el.playerName == name);
        }

        public void AddPlayerForOthersPlayers() //няхай іншыя гульцы дабавьц гэтага
        {
            if (!PhotonNetwork.IsConnected)
                return;
            photonView.RPC("AddPlayer", RpcTarget.Others, owner.playerName);
            //Thread.Sleep(1000);
            UpdatePlayerForOtherPlayers();
        }
        public void RemovePlayerForOtherPlayers()
        {
            if (!PhotonNetwork.IsConnected)
                return;
            photonView.RPC("RemovePlayer", RpcTarget.Others, owner.playerName);
        }
        public void UpdatePlayerForOtherPlayers()
        {
            if (!PhotonNetwork.IsConnected)
                return;
            string combGenJson = JsonUtility.ToJson(new CombinationGeneratorSer(owner.possibleCombinations));
            photonView.RPC("UpdatePlayerFromOtherPlayer", RpcTarget.Others,
                owner.playerName,
                combGenJson);
        }
        [PunRPC]
        private void UpdatePlayerFromOtherPlayer(string name, string combinationGeneratorJson)
        {
            var pl = GetPlayerByName(name);
            if (pl != null)
            {
                CombinationGeneratorSer ser = JsonUtility.FromJson<CombinationGeneratorSer>(combinationGeneratorJson);
                pl.possibleCombinations.Deserialize(ser);
            }
            MapController.CalculateTowerClassesForAll();
        }
    }
}
