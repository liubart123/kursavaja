using Assets.GamePlay.Scripts.Ammo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.GamePlay.Scripts.Tower.Interfaces.bulletFactory
{
    class BulletFactoryRaw : BulletFactory
    {
        public Bullet bulletFromUnityEditor;
        public override Bullet CreateBullet(BulletFactoryParameters args)
        {
            return bulletFromUnityEditor;
        }
    }
}
