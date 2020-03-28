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
            directionOfMoving.Normalize();
            GetComponent<Rigidbody2D>().velocity=(directionOfMoving * force);
            //Debug.Log(GetComponent<Rigidbody2D>().velocity);
            //GetComponent<Rigidbody2D>().AddForce(directionOfMoving * force);
            decorator.DoShot(target);
        }
        public override void Move ()
        {
            //Vector2 positionOfFLying = CalculateNextPosition();
            //SetPosition(positionOfFLying);
            OnFly(transform.position, transform.rotation);
            GetComponent<Rigidbody2D>().velocity = (directionOfMoving * force);
            //GetComponent<Rigidbody2D>().AddForce(directionOfMoving * force);
        }
        public override Vector2 CalculateNextPosition ()
        {
            return directionOfMoving * speedOfMoving + (Vector2)transform.position;
        }
        public override void SetPosition (Vector2 pos)
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
            if (other.gameObject.tag != "testTag")
            {
                int a =2 + 2;
            }
            Move();
        }
        //public void FixedUpdate()
        //{
        //    Move();
        //}

    }
}
