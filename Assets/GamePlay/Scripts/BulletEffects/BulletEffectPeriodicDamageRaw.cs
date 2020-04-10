using Assets.GamePlay.Scripts.Enemies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Assets.GamePlay.Scripts.Damage.DamageManager;

namespace Assets.GamePlay.Scripts.BulletEffects
{
    public class BulletEffectPeriodicDamageRaw : BulletEffect
    {

        EKindOfDamage kindOfDamage;
        public BulletEffectPeriodicDamageRaw(float intensity, EKindOfDamage kindOfDamage) : base(intensity)
        {
            this.kindOfDamage = kindOfDamage;
        }

        public override void AffectOnce(Enemy enemy)
        {
        }
        public override void Affect(Enemy enemy)
        {
            enemy.GetDamage(Intensity / 10, kindOfDamage);
        }

        public override void RemoveEffect(Enemy enemy)
        {
        }
    }
}
