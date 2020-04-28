using Assets.GamePlay.Scripts.Enemies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.GamePlay.Scripts.BulletEffects
{
    [Serializable]
    public abstract class BulletEffect
    {
        public string effectName;
        protected BulletEffect(float effectivity)
        {
            this.effectivity = effectivity;
        }
        public abstract BulletEffect Clone();

        public float effectivity;

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

        public ETypeOfBulletEffect typeOfEffect;
        public enum ETypeOfBulletEffect
        {
            immidiateDamage,
            periodicDamage,
            slowing
        }
    }
}
