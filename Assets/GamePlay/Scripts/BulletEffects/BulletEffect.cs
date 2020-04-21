using Assets.GamePlay.Scripts.Enemies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.GamePlay.Scripts.BulletEffects
{
    public abstract class BulletEffect
    {
        protected BulletEffect(float effectivity)
        {
            Effectivity = effectivity;
        }

        public float Effectivity { get; set; }

        //effect, that appears every fixedUpdate
        public virtual void Affect(Enemy enemy)
        {

        }

        //effect, that appears at the beginning
        public abstract void AffectOnce(Enemy enemy);


        //removing effect from the enemy
        public abstract void RemoveEffect(Enemy enemy);

        public abstract BulletEffect CloneEffectWithOtherIntensity(float effectivity);

        public override bool Equals(object obj)
        {
            return obj is BulletEffect;
        }
    }
}
