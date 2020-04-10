using Assets.GamePlay.Scripts.Ammo;
using Assets.GamePlay.Scripts.Bonuses;
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
        public abstract Bullet CreateBullet(BulletFactoryParameters args);

        protected Bullet bullet;    //type of bullet, that will be created
        public virtual void Initialize(Bullet bullet, ICollection<TowerClass> towerClasses)
        {
            this.bullet = bullet;
            this.towerClasses = towerClasses;
        }


        //TOWER_CLASSES
        protected ICollection<TowerClass> towerClasses;    //classes that are got from other towers
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
