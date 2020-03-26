using Assets.GamePlay.Scripts.Ammo;
using Assets.GamePlay.Scripts.Bonuses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.GamePlay.Scripts.Tower.Interfaces
{
    public abstract class BulletFactory : MonoBehaviour
    {
        public abstract Bullet CreateBullet(BulletFactoryParameters args);
    }
    public class BulletFactoryParameters
    {
        ICollection<Bonus> bonuses;
    }
}
