using Assets.GamePlay.Scripts.Enemies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.GamePlay.Scripts.Ammo
{
    public class BulletRaw  : Bullet
    {
        public BulletRaw ()
        {
            //DoShot = DoShot ;
            //Move = Move ;
            //SetPosition = SetPosition ;
            //SetRotation = SetRotation ;
            //SelfDestroy = SelfDestroy ;
            //Live = Live ;
            //CalculateNextPosition = CalculateNextPosition ;
        }

        public override void DoShot (Enemy target) {
            directionOfMoving = target.transform.position - transform.position;
            directionOfMoving.z = 0;
            directionOfMoving.Normalize();
            decorator.DoShot(target);
        }
        public override void Move ()
        {
            Vector3 positionOfFLying = CalculateNextPosition();
            SetPosition(positionOfFLying);
            OnFly(positionOfFLying, transform.rotation);
        }
        public override Vector3 CalculateNextPosition ()
        {
            return directionOfMoving * speedofMoving + transform.position;
        }
        public override void SetPosition (Vector3 pos)
        {
            transform.position = pos;
        }
        public override void SetRotation (Quaternion pos)
        {
            transform.rotation = pos;
        }
        public override void SelfDestroy ()
        {
            //Destroy(this.gameObject);
            
        }

        private void Start()
        {
        }

    }
}
