using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.GamePlay.Scripts.Building.interfaces.HealthContorller
{
    public class HealthControllerForBonuses : HealthController
    {
        protected override void ChangeViewAfterHealthChanging()
        {
            //transform.GetComponentInChildren<SpriteRenderer>().color
            //        = Color.Lerp(Color.black, colorFOrFullHealth, health / maxHealth);
        }
        public override float Damage
        {
            get { return damage; }
            set
            {
                damage = value;
            }
        }
        public override void Initialize()
        {
            colorFOrFullHealth = transform.GetComponentInChildren<SpriteRenderer>().color;
            damage = 0;
        }
    }
}
