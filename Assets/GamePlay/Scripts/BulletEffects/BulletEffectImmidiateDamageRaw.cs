using Assets.GamePlay.Scripts.Enemies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Assets.GamePlay.Scripts.Damage.DamageManager;

namespace Assets.GamePlay.Scripts.BulletEffects
{
    class BulletEffectImmidiateDamageRaw : BulletEffect
    {
        EKindOfDamage kindOfDamage;
        public BulletEffectImmidiateDamageRaw(float intensity, EKindOfDamage kindOfDamage) : base(intensity)
        {
            this.kindOfDamage = kindOfDamage;
        }

        public override void AffectOnce(Enemy enemy)
        {
            enemy.GetDamage(Effectivity, kindOfDamage);
            enemy.RemoveEffect(this);
        }

        public override void RemoveEffect(Enemy enemy)
        {
        }
        public override BulletEffect CloneEffectWithOtherIntensity(float Intensity)
        {
            return new BulletEffectImmidiateDamageRaw(Intensity, kindOfDamage);
        }

        public override bool Equals(object obj)
        {
            return obj is BulletEffectImmidiateDamageRaw raw &&
                   base.Equals(obj) &&
                   kindOfDamage == raw.kindOfDamage;
        }
    }
}

