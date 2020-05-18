using Assets.GamePlay.Scripts.Building.interfaces.HealthContorller;
using Assets.GamePlay.Scripts.Enemies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Assets.GamePlay.Scripts.Building;
using Photon.Pun;

namespace Assets.GamePlay.Scripts.Bonuses
{
    public class Bonus : Building.Building
    {
        //private void Start()
        //{
        //    Debug.Log("bonus was created");
        //}
        public enum EBonusType
        {
            red,green,blue, neutral
        }

        public EBonusType bonusType;


        //HEALTH
        public HealthController healthController;
        public void TakeDamage(float damage)
        {
            healthController.Damage += damage;
        }

        private void OnTriggerEnter(Collider other)
        {
            other.GetComponent<Enemy>().DoDamage(this);
        }


        protected virtual void OnTriggerEnter2D(Collider2D collision)
        {
            if (OnlineManager.DoNotOwnCalculations)
                return;
            collision.gameObject?.GetComponent<Enemy>()?.DoDamage(this);
        }

        public override void Initialize()
        {
            if (OnlineManager.DoNotOwnCalculations)
                return;
            base.Initialize();
            healthController = GetComponent<HealthController>();
            healthController.Initialize();
        }
    }
}
