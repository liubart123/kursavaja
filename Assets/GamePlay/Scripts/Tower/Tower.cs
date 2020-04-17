using Assets.GamePlay.Scripts.Ammo;
using Assets.GamePlay.Scripts.Enemies;
using Assets.GamePlay.Scripts.Tower.Interfaces;
using Assets.GamePlay.Scripts.TowerClasses;
using Assets.GamePlay.Scripts.TowerClasses.TowerCombinations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Assets.GamePlay.Scripts.Player;
using Assets.GamePlay.Scripts.Tower.Interfaces.ClassesCollection;
using Assets.GamePlay.Scripts.BulletEffects;

namespace Assets.GamePlay.Scripts.Tower
{
    public abstract class Tower : Building.Building
    {

        [SerializeField]
        protected Bullet bullet;   //type of bullet, that is used by this tower
        public float Effectivity { get; protected set; }    //effectivity of effects

        //AIMING
        public Enemy CurrentTarget { get; protected set; }
        private Vector2 directionOfShooting;
        public TargetChooser TargetChooser{ get; protected set; }
        public virtual void ChooseTarget()
        {
            //if (TargetChooser == null)
            //    return;
            CurrentTarget = TargetChooser.ChooseTarget(
                new TargetChooserParameters(
                    transform.eulerAngles.z,
                    (Vector2)transform.position));
            AimTaker.ResetAimTaker();
            if (CurrentTarget != null)
            {
                CurrentTarget.eventsWhenThisDie += en => ChooseTarget();
            }
        }   //chose target among all possible enemies
        public AimTaker AimTaker { get; protected set; }    //calculate direction for shooting
        public virtual void TakeAim()
        {
            if (CurrentTarget == null)
            {
                return;
            }
            directionOfShooting = AimTaker.TakeAim(new AimTakerParameters(CurrentTarget,transform.position, bullet.speedOfMoving));
            bool aimed = TowerRotater.RotateTower(new TowerRotaterParameters(directionOfShooting, transform));
            if (aimed && isLoaded)
            {
                Shoot();
            }
        }   //calculate rigth direction and rotate tower accordingly
        public TowerRotater TowerRotater { get; protected set; }

        //SHOOTING
        public Shooter Shooter { get; protected set; }
        public virtual void Shoot()
        {
            Bullet bullet = BulletFactory.CreateBullet(new BulletFactoryParameters(transform));
            Shooter.Shoot(new ShooterParameters(CurrentTarget, bullet));
            ResetReloading();
        }

        //RELOADING
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
        protected virtual void InitializeBulletFactory()
        {
            ICollection<BulletEffect> effects = classCollection.GetAllEffects();
            BulletFactory.Initialize(bullet, effects, this);
        }

        //TOWER_CLASSES
        public ClassCollection classCollection;

        private bool initialized = false;
        public override void Initialize()
        {
            initialized = true;
            base.Initialize();
        }
        public void FixedUpdate()
        {
            if (initialized)
            {
                Reload();
                TakeAim();
            }
        }
    }
}
