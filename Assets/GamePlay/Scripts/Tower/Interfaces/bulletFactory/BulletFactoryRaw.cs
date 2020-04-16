using Assets.GamePlay.Scripts.Ammo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Assets.GamePlay.Scripts.Other.ObjectPull;
using Assets.GamePlay.Scripts.TowerClasses;
using Assets.GamePlay.Scripts.BulletEffects;

namespace Assets.GamePlay.Scripts.Tower.Interfaces.bulletFactory
{
    class BulletFactoryRaw : BulletFactory
    {
        protected IObjectPull<Bullet> pull;  //pull of bullets
        [SerializeField]
        protected int pullSize;     
        public void Start()
        {
        }
        public override void Initialize(Bullet bullet, ICollection<BulletEffect> effects, Tower tower)
        {
            base.Initialize(bullet, effects, tower);
            bullet.ListOfEffects = effects;

            pull = GetComponent<BulletPull>();
            pull.Initialize(bullet.gameObject, pullSize);
        }   
        public override Bullet CreateBullet(BulletFactoryParameters args)
        {
            Bullet bul = pull.GetObject();
            bul.transform.position = args.towerTransform.position;
            bul.transform.rotation = args.towerTransform.rotation;
            //Bullet bul = Instantiate(bulletFromUnityEditor, args.towerTransform.position, args.towerTransform.rotation);
            //EffectDecorator effect = bul.gameObject.AddComponent<FireEffect>();
            //(effect as FireEffect).fireObjFromUnity = fireFromUnityEditor;
            //bul.decorator = effect;
            return bul;
        }

        //private void Update()
        //{
        //    if (Input.GetKeyDown(KeyCode.Space))
        //    {
        //        bullet.speedOfMoving = 50;
        //        pull.ClearPool();
        //        pull.Initialize(bullet.gameObject, pullSize);
        //    }
        //}
    }
}
