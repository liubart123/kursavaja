using Assets.GamePlay.Scripts.BulletEffects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Assets.GamePlay.Scripts.Damage.DamageManager;

namespace Assets.GamePlay.Scripts.TowerClasses
{
    public class TowerClassRaw4 : TowerClass
    {
        public TowerClassRaw4()
        {
            BulletEffects.Add(new BulletEffectPeriodicDamageRaw(2, EKindOfDamage.blue));
            Id = 4;
        }
    }
}
