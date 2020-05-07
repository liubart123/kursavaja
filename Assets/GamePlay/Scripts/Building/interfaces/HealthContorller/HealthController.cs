using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.GamePlay.Scripts.Building.interfaces.HealthContorller
{
    public class HealthController : MonoBehaviour
    {
        [SerializeField]
        protected float maxHealth;
        [SerializeField]
        protected float damage;
        public virtual float Damage
        {
            get { return damage; }
            set
            {
                damage = value;
                if (damage < 0)
                {
                    Die();
                }
                ChangeViewAfterHealthChanging();
            }
        }
        [SerializeField]
        protected Color colorFOrFullHealth;
        protected virtual void ChangeViewAfterHealthChanging()
        {
            GetComponent<SpriteRenderer>().color
                    = Color.Lerp(Color.black, colorFOrFullHealth, damage / maxHealth);
        }
        public virtual void Die()
        {
            GetComponent<Building>().Die();
            //Destroy(this.gameObject);
            //gameObject.SetActive(false);
        }
        public virtual void Initialize()
        {
            colorFOrFullHealth = GetComponent<SpriteRenderer>().color;
            damage = maxHealth;
        }
    }
}
