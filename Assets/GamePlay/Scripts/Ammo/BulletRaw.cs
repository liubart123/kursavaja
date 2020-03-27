using Assets.GamePlay.Scripts.Enemies;
using Assets.GamePlay.Scripts.Other.ObjectPull;
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
        [SerializeField]
        protected float force;
        [SerializeField]
        protected float timeToLive;
        protected Enemy currentTarget;
        public override void DoShot (Enemy target) {
            //Destroy(this.gameObject, timeToLive);
            currentTarget = target;
            directionOfMoving = target.transform.position - transform.position;
            directionOfMoving.z = 0;
            directionOfMoving.Normalize();
            GetComponent<Rigidbody>().velocity=(directionOfMoving * force);
            //Debug.Log(GetComponent<Rigidbody>().velocity);
            //GetComponent<Rigidbody>().AddForce(directionOfMoving * force);
            decorator.DoShot(target);
        }
        public override void Move ()
        {
            //Vector3 positionOfFLying = CalculateNextPosition();
            //SetPosition(positionOfFLying);
            OnFly(transform.position, transform.rotation);
            GetComponent<Rigidbody>().velocity = (directionOfMoving * force);
            //GetComponent<Rigidbody>().AddForce(directionOfMoving * force);
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

        public override void Delete()
        {
            //GetComponent<BulletForPull>().ReturnToPull();
        }
        private void OnTriggerEnter(Collider other)
        {
            Move();
        }
        //public void FixedUpdate()
        //{
        //    Move();
        //}

    }
}
