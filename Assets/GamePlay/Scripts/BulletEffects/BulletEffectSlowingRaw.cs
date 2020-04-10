using Assets.GamePlay.Scripts.Enemies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.GamePlay.Scripts.BulletEffects
{
    public class BulletEffectSlowingRaw : BulletEffect
    {
        public BulletEffectSlowingRaw(float intensity) : base(intensity)
        {
        }

        public override void AffectOnce(Enemy enemy)
        {
            enemy.speed /= Intensity;
        }

        public override void RemoveEffect(Enemy enemy)
        {
            enemy.speed *= Intensity;
        }
    }
}
