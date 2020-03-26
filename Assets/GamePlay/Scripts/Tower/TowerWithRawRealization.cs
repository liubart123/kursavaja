using Assets.GamePlay.Scripts.Ammo;
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
            TargetChooser = GetComponent<TargetChooser>();
            AimTaker = GetComponent<AimTaker>();
            TowerRotater = GetComponent<TowerRotater>();
            Shooter = GetComponent<Shooter>();
            Reloader = GetComponent<Reloader>();
            BulletFactory = GetComponent<BulletFactory>();
            ChooseTarget();
        }
    }
}
