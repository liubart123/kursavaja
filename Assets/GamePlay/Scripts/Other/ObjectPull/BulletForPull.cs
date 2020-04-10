using Assets.GamePlay.Scripts.Ammo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.GamePlay.Scripts.Other.ObjectPull
{
    class BulletForPull : MonoBehaviour
    {
        protected IObjectPull<Bullet> pull;

        public void SetPull(IObjectPull<Bullet> pull)
        {
            this.pull = pull;
        }
        public void ReturnToPull()
        {
            Bullet currentBullet = GetComponent<Bullet>();
            currentBullet.Delete();
            pull.ReturnObject(currentBullet);
        }
    }
}
