using Assets.GamePlay.Scripts.Map;
using Photon.Pun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static Assets.GamePlay.Scripts.Bonuses.Bonus;

namespace Assets.GamePlay.Scripts.Bonuses
{
    public class BonusesBuilder : MonoBehaviour
    {
        public Sprite[] bonusSprites;
        public Player.MyPlayer owner;
        private PhotonView photonView;
        public void Initialize(Player.MyPlayer pl)
        {
            owner = pl;
            photonView = GetComponent<PhotonView>();
        }
        public Action OnBonusChanging;
        public void ChangeTypeOfBonus(EBonusType type, Bonus bonus, bool invokeActionsAfterChanging = true)
        {
            if (bonus != null)
            {
                Block blockOfBonus = bonus.GetBlock();
                if (OnlineManager.BuildForAllPlayers == true)
                {
                    photonView.RPC("ChangeTypeOfBonusForOther",
                        RpcTarget.Others,
                        (int)type,
                        new Vector3(blockOfBonus.indexes.x, blockOfBonus.indexes.y,0));
                }
                bonus.bonusType = type;
                bonus.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = bonusSprites[(int)type];
                bonus.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = Color.white;
                //MapController.CalculateTowerClassesForAll();
                if (invokeActionsAfterChanging)
                    OnBonusChanging?.Invoke();
            }
        }
        [PunRPC]
        public void ChangeTypeOfBonusForOther(int type, Vector3 bonus)
        {
            Bonus bon = BlocksGenerator.
                GetBlock(new Vector2Int((int)bonus.x, (int)bonus.y)).
                GetBuilding().GetComponent<Bonus>();
            ChangeTypeOfBonus((EBonusType)type, bon);
        }
    }
}
