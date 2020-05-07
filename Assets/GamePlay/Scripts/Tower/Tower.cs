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
using Assets.GamePlay.Scripts.Building.interfaces.HealthContorller;
using Assets.GamePlay.Scripts.Tower.auxil;
using Assets.GamePlay.Scripts.Tower.Interfaces.BonusConveyor;
using Photon.Pun;

namespace Assets.GamePlay.Scripts.Tower
{
    public abstract class Tower : Building.Building
    {

        [SerializeField]
        protected Bullet bullet;   //type of bullet, that is used by this tower
        [SerializeField]
        protected float effectivity;   //effectivity of effects

        public string towerName;

        //AIMING
        public TargetPool targetPool;
        public Enemy CurrentTarget { get; protected set; }
        private Vector2 directionOfShooting;
        public TargetChooser TargetChooser{ get; protected set; }
        public virtual void ChooseTarget()
        {
            CurrentTarget = TargetChooser.ChooseTarget(
                new TargetChooserParameters(
                    (transform.eulerAngles.z + 90)/180*Mathf.PI,
                    (Vector2)transform.position));
            AimTaker.ResetAimTaker();
        }   //chose target among all possible enemies

        //пасля смерці тавэра цякучы таргет не будзе аднаўляць тагрет тафэру пасля смерці сябе)
        public AimTaker AimTaker { get; protected set; }    //calculate direction for shooting
        public virtual void TakeAim(bool getActualValue = false)
        {
            if (CurrentTarget == null || CurrentTarget.gameObject.activeSelf == false)
            {
                ChooseTarget();
                return;
            }
            directionOfShooting = AimTaker.TakeAim(new AimTakerParameters(CurrentTarget,transform.position, bullet.speedOfMoving, getActualValue));
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
            TakeAim(true);
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
        public virtual void InitializeBulletFactory()
        {
            InitializeBullet();
            ICollection<BulletEffect> effects = classCollection.GetAllEffects();
            foreach(var ef in effects)
            {
                ef.effectivity *= effectivity;
            }
            BulletFactory.Initialize(bullet, effects, this);
        }
        protected virtual void InitializeBullet()
        {
            bullet = transform.GetComponentInChildren<Bullet>(true);
        }

        //TOWER_CLASSES
        public ClassCollection classCollection;
        public BonusConveyor bonusConveyor;

        private bool initialized = false;
        public virtual void ResetState()
        {
            InitializeBulletFactory();
            AimTaker.ResetAimTaker();
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        public override void Initialize()
        {
            if (OnlineManager.DoNotOwnCalculations)
                return;
            initialized = true;

            targetPool = transform.GetComponentInChildren<TargetPool>();
            //Owner = FindObjectOfType<Player.MyPlayer>();
            TargetChooser = GetComponent<TargetChooser>();
            AimTaker = GetComponent<AimTaker>();
            TowerRotater = GetComponent<TowerRotater>();
            Shooter = GetComponent<Shooter>();
            Reloader = GetComponent<Reloader>();
            BulletFactory = GetComponent<BulletFactory>();
            classCollection = GetComponent<ClassCollection>();
            bonusConveyor = GetComponent<BonusConveyor>();


            classCollection.Initialize();
            targetPool.Initialize();
            TargetChooser.Initialize();
            //InitializeBulletFactory();
            bonusConveyor.Initialize();



            base.Initialize();
            //GetBlock().passability = Mathf.Infinity;
        }
        public void FixedUpdate()
        {
            if (OnlineManager.DoNotOwnCalculations)
                return;
            if (initialized)
            {
                Reload();
                TakeAim();
            }
        }

        public override void Die()
        {
            BulletFactory.Delete();
            base.Die();
            classCollection.Die();
        }

        //SHOWING TOWER INFORMATION ON MAP
        public void ShowTowerInfo()
        {
            bonusConveyor.ShowConveyor();

            var blocks = classCollection.GetBlocksInRange();
            foreach(var b in blocks)
            {
                b.LightBlockUp();
            }
        }
        public void HideTowerInfo()
        {
            bonusConveyor.HideConveyor();
            var blocks = classCollection.GetBlocksInRange();
            foreach (var b in blocks)
            {
                b.UnLightBlock();
            }
        }
        //public void Update()
        //{
        //    if (Input.GetKeyDown(KeyCode.Space))
        //    {
        //        //string json = JsonUtility.ToJson(classCollection.defaultTowerClass.BulletEffects.ElementAt(0));
        //        //var ef = JsonUtility.FromJson<BulletEffectImmidiateDamageRaw>(json);
        //        string json = classCollection.defaultTowerClass.BulletEffects.ElementAt(0).Serialize();
        //        var asd = BulletEffect.DeSerialize(json);
        //    }
        //}
    }
}
