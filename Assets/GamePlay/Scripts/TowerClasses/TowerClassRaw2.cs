using Assets.GamePlay.Scripts.BulletEffects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Assets.GamePlay.Scripts.Damage.DamageManager;

namespace Assets.GamePlay.Scripts.TowerClasses
{
    public class TowerClassRaw2 : TowerClass
    {
        public TowerClassRaw2()
        {
            BulletEffects.Add(new BulletEffectSlowingRaw(2));
            Id = 2;
        }
    }
}
