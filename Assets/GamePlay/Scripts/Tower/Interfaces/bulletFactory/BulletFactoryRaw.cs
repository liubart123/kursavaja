using Assets.GamePlay.Scripts.Ammo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.GamePlay.Scripts.Effect;
using UnityEngine;

namespace Assets.GamePlay.Scripts.Tower.Interfaces.bulletFactory
{
    class BulletFactoryRaw : BulletFactory
    {
        public Bullet bulletFromUnityEditor;
        //public GameObject fireFromUnityEditor;
        public override Bullet CreateBullet(BulletFactoryParameters args)
        {
            Bullet bul = Instantiate(bulletFromUnityEditor.gameObject, 
                args.towerTransform.position, 
                args.towerTransform.rotation).GetComponent<Bullet>();
            //EffectDecorator effect = bul.gameObject.AddComponent<FireEffect>();
            //(effect as FireEffect).fireObjFromUnity = fireFromUnityEditor;
            //bul.decorator = effect;
            return bul;
        }
    }
}
