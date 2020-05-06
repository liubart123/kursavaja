using Assets.GamePlay.Scripts.Enemies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static Assets.GamePlay.Scripts.Damage.DamageManager;

namespace Assets.GamePlay.Scripts.BulletEffects
{
    [Serializable]
    class BulletEffectImmidiateDamageRaw : BulletEffect
    {
        public EKindOfDamage kindOfDamage;
        public BulletEffectImmidiateDamageRaw(float intensity, EKindOfDamage kindOfDamage) : base(intensity)
        {
            this.kindOfDamage = kindOfDamage;
            typeOfEffect = ETypeOfBulletEffect.immidiateDamage;
            effectName = "immidiateDamage";
        }

        public override void AffectOnce(Enemy enemy)
        {
            enemy.GetDamage(effectivity, kindOfDamage);
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
        public override BulletEffect Clone()
        {
            return new BulletEffectImmidiateDamageRaw(effectivity, kindOfDamage);
        }
        public override string Serialize()
        {
            return JsonUtility.ToJson(new EffectToSer(typeOfEffect, this));
        }
    }
}

