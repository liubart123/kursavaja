﻿using Assets.GamePlay.Scripts.Ammo;
using Assets.GamePlay.Scripts.Enemies;
using Assets.GamePlay.Scripts.Tower.Interfaces;
using Assets.GamePlay.Scripts.Tower.Interfaces.aimRotater;
using Assets.GamePlay.Scripts.Tower.Interfaces.targetChooser;
using Assets.GamePlay.Scripts.Tower.Interfaces.bulletFactory;
using Assets.GamePlay.Scripts.Tower.Interfaces.reloader;
using Assets.GamePlay.Scripts.Tower.Interfaces.shooter;
using Assets.GamePlay.Scripts.Tower.Interfaces.towerRotater;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Assets.GamePlay.Scripts.TowerClasses;
using Assets.GamePlay.Scripts.TowerClasses.TowerCombinations;
using Assets.GamePlay.Scripts.Tower.Interfaces.ClassesCollection;

namespace Assets.GamePlay.Scripts.Tower
{
    //class with raw realizations for Tower class delegatos si
    public class TowerWithRawRealization : Tower
    {
        public TowerWithRawRealization()
        {
        }

        public void Start()
        {
            //tower's interfaces
            owner = FindObjectOfType<Player.Player>();
            TargetChooser = GetComponent<TargetChooser>();
            AimTaker = GetComponent<AimTaker>();
            TowerRotater = GetComponent<TowerRotater>();
            Shooter = GetComponent<Shooter>();
            Reloader = GetComponent<Reloader>();
            BulletFactory = GetComponent<BulletFactory>();
            classCollection = GetComponent<ClassCollection>();

            //effectivity
            Effectivity = 1;

            classCollection.Initialize();
            InitializeBulletFactory();


        }

        public override void Shoot()
        {
            Bullet bullet = BulletFactory.CreateBullet(new BulletFactoryParameters(transform));
            Shooter.Shoot(new ShooterParameters(
                AimTaker.TakeAim(new AimTakerParameters(CurrentTarget, transform.position, bullet.speedOfMoving, true)),
                bullet));
            ResetReloading();
        }
    }
}
