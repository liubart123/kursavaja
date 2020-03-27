using Assets.GamePlay.Scripts.Ammo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.GamePlay.Scripts.Effect;
using UnityEngine;
using Assets.GamePlay.Scripts.Other.ObjectPull;

namespace Assets.GamePlay.Scripts.Tower.Interfaces.bulletFactory
{
    class BulletFactoryRaw : BulletFactory
    {
        protected BulletPull pull;
        [SerializeField]
        protected int pullSize;
        public Bullet bulletFromUnityEditor;
        //public GameObject fireFromUnityEditor;
        public void Start()
        {
            //pull = gameObject.AddComponent<BulletPull>();
            pull = GetComponent<BulletPull>();
            pull.Initialize(bulletFromUnityEditor.gameObject, pullSize);
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
    }
}
