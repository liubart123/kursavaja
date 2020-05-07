using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.GamePlay.Scripts.Enemies.Interfaces.Damager
{
    public class DamageMakerRaw : DamageMaker
    {
        public override void DoDamage(DamageMakerPatameters args)
        {
            args.target.TakeDamage(damage);
        }
        //private void OnTriggerEnter2D(UnityEngine.Collider2D collision)
        //{
        //    Building.Building tower = collision.gameObject.GetComponent<Building.Building>();
        //    if (tower != null)
        //    {

        //    }
        //}
    }
}
