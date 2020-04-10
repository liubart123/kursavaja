using Assets.GamePlay.Scripts.BulletEffects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Assets.GamePlay.Scripts.Damage.DamageManager;

namespace Assets.GamePlay.Scripts.TowerClasses
{
    public class TowerClassRaw1 : TowerClass
    {
        public TowerClassRaw1()
        {
            BulletEffects.Add(new BulletEffectImmidiateDamageRaw(20,EKindOfDamage.blue));
            Id = 1;
        }
    }
}
