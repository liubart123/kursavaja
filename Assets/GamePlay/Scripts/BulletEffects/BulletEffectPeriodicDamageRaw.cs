using Assets.GamePlay.Scripts.Enemies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Assets.GamePlay.Scripts.Damage.DamageManager;

namespace Assets.GamePlay.Scripts.BulletEffects
{
    [Serializable]
    public class BulletEffectPeriodicDamageRaw : BulletEffect
    {

        public EKindOfDamage kindOfDamage;
        public BulletEffectPeriodicDamageRaw(float intensity, EKindOfDamage kindOfDamage) : base(intensity)
        {
            this.kindOfDamage = kindOfDamage;
            typeOfEffect = ETypeOfBulletEffect.periodicDamage;
            effectName = "periodicDamage";
        }

        public override void AffectOnce(Enemy enemy)
        {
        }
        public override void Affect(Enemy enemy)
        {
            enemy.GetDamage(effectivity, kindOfDamage);
        }

        public override void RemoveEffect(Enemy enemy)
        {
        }

        public override BulletEffect CloneEffectWithOtherIntensity(float Intensity)
        {
            return new BulletEffectPeriodicDamageRaw(Intensity, kindOfDamage);
        }

        public override bool Equals(object obj)
        {
            return obj is BulletEffectPeriodicDamageRaw raw &&
                   base.Equals(obj) &&
                   kindOfDamage == raw.kindOfDamage;
        }

        public override BulletEffect Clone()
        {
            return new BulletEffectPeriodicDamageRaw(effectivity, kindOfDamage);
        }
    }
}
