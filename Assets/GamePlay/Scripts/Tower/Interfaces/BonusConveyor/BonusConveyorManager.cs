using Assets.GamePlay.Scripts.Bonuses;
using Assets.GamePlay.Scripts.Player;
using Photon.Pun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static Assets.GamePlay.Scripts.Bonuses.Bonus;

namespace Assets.GamePlay.Scripts.Tower.Interfaces.BonusConveyor
{
    public class BonusConveyorManager : MonoBehaviour
    {
        private MyPlayer owner;
        public void Initialize(MyPlayer pl)
        {
            owner = pl;
        }
        public PhotonView photonView;
        public void OnAddBonusForOtherPlayers(Vector2 bonus, Vector2 block)
        {
            if (PhotonNetwork.IsConnected)
            {
                photonView.RPC("AddBonusForOtherPlayers",
                    RpcTarget.Others,
                    new Vector3(bonus.x, bonus.y, 0),
                    new Vector3(block.x, block.y, 0));
            }
        }
        [PunRPC]
        public void AddBonusForOtherPlayers(Vector3 bonus, Vector3 block)
        {
            Tower tower = BlocksGenerator.GetBlock(new Vector2Int(
                (int)block.x,
                (int)block.y)).GetBuilding().GetComponent<Tower>();
            Bonus b = BlocksGenerator.GetBlock(new Vector2Int(
                (int)bonus.x,
                (int)bonus.y)).GetBuilding().GetComponent<Bonus>();
            tower.bonusConveyor.AddBonus(b);
        }
    }
}
