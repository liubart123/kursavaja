using Assets.GamePlay.Scripts.Bonuses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.GamePlay.Scripts.Enemies.Interfaces.Damager
{
    public abstract class DamageMaker : MonoBehaviour
    {
        public float damage;
        public abstract void DoDamage(DamageMakerPatameters args);
    }
    public class DamageMakerPatameters
    {
        public Bonus target;

        public DamageMakerPatameters(Bonus target)
        {
            this.target = target;
        }
    }
}
