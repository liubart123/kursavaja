using Assets.GamePlay.Scripts.Ammo;
using Assets.GamePlay.Scripts.Bonuses;
using Assets.GamePlay.Scripts.BulletEffects;
using Assets.GamePlay.Scripts.TowerClasses;
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
        Tower tower;

        public abstract Bullet CreateBullet(BulletFactoryParameters args);

        protected Bullet bullet;    //type of bullet, that will be created
        public virtual void Initialize(Bullet bullet, ICollection<BulletEffect> effects, Tower tower )
        {
            this.bullet = bullet;
            this.effects = effects;
            this.tower = tower;
        }

        public abstract void Delete();

        //TOWER_CLASSES
        protected ICollection<BulletEffect> effects;    //classes that are got from other towers
    }
    public class BulletFactoryParameters
    {
        public Transform towerTransform;

        public BulletFactoryParameters(Transform towerTransform)
        {
            this.towerTransform = towerTransform;
        }
    }
}
