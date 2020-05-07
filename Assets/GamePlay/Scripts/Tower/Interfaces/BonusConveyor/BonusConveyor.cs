using Assets.GamePlay.Scripts.Bonuses;
using Assets.GamePlay.Scripts.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.GamePlay.Scripts.Tower.Interfaces.BonusConveyor
{
    public class BonusConveyor : MonoBehaviour
    {
        public MyPlayer realPlayer;
        public ICollection<Bonus> bonuses = new List<Bonus>();
        public GameObject objectForDrawingLines;
        public void ShowConveyor()
        {
            if (bonuses.Count == 0)
            {
                return;
            }
            transform.GetChild(2).gameObject.SetActive(true);
            var line = transform.GetChild(2).gameObject.GetComponent<LineRenderer>();
            
            Vector3[] poss = new Vector3[4];
            poss[0] = transform.position;
            int i = 1;
            foreach(var b in bonuses)
            {
                poss[i] = b.GetBlock().transform.position;
                i++;
            }
            line.positionCount = bonuses.Count+1;
            line.SetPositions(poss);
            int hash = Mathf.Abs(GetHashCode());
            Color color = new Color((hash % 1000)/1000f, ((hash + 12345) % 1000)/1000f, ((hash * 431) % 1000) / 1000f);
            line.startColor = color;
            line.endColor = color;
        }
        public void HideConveyor()
        {
            if (this == null)
            {
                return;
            }
            transform?.GetChild(2).gameObject.SetActive(false);
        }
        public void ResetConveyor()
        {
            bonuses.Clear();
            HideConveyor();
            GetComponent<Tower>().classCollection.OnBonusTowerClassChange();
        }
        public void AddBonus(Bonus bonus)
        {
            if (bonus.bonusType == Bonus.EBonusType.neutral)
            {
                return;
            }
            if (bonuses.Count >= 3)
            {
                return;
            }
            if (bonuses.Contains(bonus))
            {
                return;
            }
            if (tower.Owner.bonusConveyorManager != null)
            {
                tower.Owner.bonusConveyorManager.OnAddBonusForOtherPlayers(
                    bonus.GetBlock().indexes, tower.GetBlock().indexes);
            } else
            {
                realPlayer.bonusConveyorManager.OnAddBonusForOtherPlayers(
                    bonus.GetBlock().indexes, tower.GetBlock().indexes);
            }

            bonuses.Add(bonus);
            if (bonuses.Count == 3)
            {
                GetComponent<Tower>().classCollection.OnBonusTowerClassChange();
            }
        }

        private Tower tower;
        public void Initialize()
        {
            tower = GetComponent<Tower>();
            objectForDrawingLines = transform.GetChild(2).gameObject;
            if (tower.Owner.isItRealPlayer == false)
            {
                realPlayer = FindObjectOfType<Players>().owner;
            }
        }
    }
}
