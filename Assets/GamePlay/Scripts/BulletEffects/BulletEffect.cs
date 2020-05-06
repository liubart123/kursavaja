using Assets.GamePlay.Scripts.Enemies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

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

        public virtual string Serialize()
        {
            return JsonUtility.ToJson(new EffectToSer(typeOfEffect,this));
        }
        public static BulletEffect DeSerialize(string json)
        {
            var efSer = JsonUtility.FromJson<EffectToSer>(json);
            //switch (efSer.typeOfEffect)
            //{
            //    case ETypeOfBulletEffect.immidiateDamage:

            //}
            return efSer.effect;
        }
        [Serializable]
        protected class EffectToSer
        {
            public ETypeOfBulletEffect typeOfEffect;
            [SerializeReference]
            public BulletEffect effect;
            public EffectToSer()
            {

            }
            public EffectToSer(ETypeOfBulletEffect typeOfEffect, BulletEffect effect)
            {
                this.typeOfEffect = typeOfEffect;
                this.effect = effect;
            }
        }
    }
}
