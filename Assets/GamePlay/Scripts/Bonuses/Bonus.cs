using Assets.GamePlay.Scripts.Building.interfaces.HealthContorller;
using Assets.GamePlay.Scripts.Enemies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Assets.GamePlay.Scripts.Building;

namespace Assets.GamePlay.Scripts.Bonuses
{
    public class Bonus : Building.Building
    {
        public enum EBonusType
        {
            red,green,blue, neutral
        }

        public EBonusType bonusType;


        //HEALTH
        public HealthController healthController;
        public void TakeDamage(float damage)
        {
            healthController.Health -= damage;
        }

        //DYING
        public event Action<Building.Building> OnDying;

        protected virtual void OnTriggerEnter2D(Collider2D collision)
        {
            collision.gameObject?.GetComponent<Enemy>()?.DoDamage(this);
        }

        public override void Initialize()
        {
            base.Initialize();
            healthController = GetComponent<HealthController>();
            healthController.Initialize();
        }
    }
}
