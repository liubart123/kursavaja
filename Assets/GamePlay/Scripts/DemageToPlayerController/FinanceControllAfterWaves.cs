using Assets.GamePlay.Scripts.Bonuses;
using Assets.GamePlay.Scripts.Player;
using Assets.GamePlay.Scripts.Waves;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.GamePlay.Scripts.DemageToPlayerController
{
    public class FinanceControllAfterWaves : MonoBehaviour
    {
        MyPlayer owner;
        [HideInInspector]
        public int minDamageForDestroing = 50;
        [HideInInspector]
        public int moneyIncrease = 100;
        public void Initialize(MyPlayer pl)
        {
            minDamageForDestroing = 50;
            moneyIncrease = 100;
            owner = pl;
            WaveManager.OnWaveEnd += DoDamage;
            WaveManager.OnWaveEnd += IncreaseMoney;
        }
        //пралічыць ўрон ўсім бонусам пасля хвалі
        private void DoDamage()
        {
            var bonuses = FindObjectsOfType<Bonus>();
            foreach(var bonus in bonuses)
            {
                DeleteTowers(bonus);
                owner.buildingsStorage.Money -= (int)bonus.healthController.Damage;
                bonus.healthController.Damage = 0;
            }
            if (owner.buildingsStorage.Money <= 0)
            {
                owner.level.GameOver();
            }
        }
        private void DeleteTowers(Bonus bonus)
        {
            int layerMask = 1 << 13;
            float totalDamage = bonus.healthController.Damage;
            var hits = Physics2D.CircleCastAll(bonus.GetBlock().transform.position, Mathf.Infinity, Vector2.zero, Mathf.Infinity, layerMask);
            foreach (var hit in hits)
            {
                if (totalDamage < minDamageForDestroing)
                    break;
                hit.collider.gameObject.GetComponent<Tower.Tower>().Die();
                totalDamage -= minDamageForDestroing;
            }
        }
        private void IncreaseMoney()
        {
            owner.buildingsStorage.Money += moneyIncrease;
        }
    }
}
