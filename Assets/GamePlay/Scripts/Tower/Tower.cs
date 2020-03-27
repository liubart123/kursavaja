using Assets.GamePlay.Scripts.Ammo;
using Assets.GamePlay.Scripts.Enemies;
using Assets.GamePlay.Scripts.Tower.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


namespace Assets.GamePlay.Scripts.Tower
{
    public abstract class Tower : Building.Building
    {
        public List<Bullet> Ammunition = new List<Bullet>();
        public Enemy CurrentTarget { get; protected set; }

        //objects and mehtods for most important tower functions
        //that can contain difficult logic
        public TargetChooser TargetChooser{ get; protected set; }
        public virtual void ChooseTarget()
        {
            CurrentTarget = TargetChooser.ChooseTarget(new TargetChooserParameters(transform.eulerAngles.z));
        }   //chose target among all possible enemies
        public AimTaker AimTaker { get; protected set; }    //calculate direction for shooting
        public virtual void TakeAim()
        {
            Vector3 directionOfShooting = AimTaker.TakeAim(new AimTakerParameters(CurrentTarget,transform.position));
            bool aimed = TowerRotater.RotateTower(new TowerRotaterParameters(directionOfShooting, transform));
            if (aimed && isLoaded)
            {
                Shoot();
            }
        }   //calculate rigth direction and rotate tower accordingly
        public TowerRotater TowerRotater { get; protected set; }
        public Shooter Shooter { get; protected set; }
        public virtual void Shoot()
        {
            Bullet bullet = BulletFactory.CreateBullet(new BulletFactoryParameters(transform));
            Shooter.Shoot(new ShooterParameters(CurrentTarget, bullet));
            ResetReloading();
        }
        public Reloader Reloader { get; protected set; }
        public Boolean isLoaded;
        public void Reload()
        {
            isLoaded = Reloader.Reload(new ReloaderParameters(ref isLoaded));
        }   //call iteration of reloading, if reloading is finished "isLoaded" will turn true
        protected void ResetReloading()
        {
            Reloader.ResetReloading(new ReloaderParameters(ref isLoaded));
            isLoaded = false;
        }
        public BulletFactory BulletFactory { get; protected set; }


        public void FixedUpdate()
        {
            Reload();
            TakeAim();
        }
    }
}
