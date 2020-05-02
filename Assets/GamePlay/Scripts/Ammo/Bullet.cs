using Assets.GamePlay.Scripts.BulletEffects;
using Assets.GamePlay.Scripts.Enemies;
using Assets.GamePlay.Scripts.Other.Cloneable;
using Assets.GamePlay.Scripts.Other.TimeForLiving;
using Photon.Pun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.GamePlay.Scripts.Ammo
{
    public abstract class Bullet : MonoBehaviour, IDeletable, IMyCloneable<Bullet>
    {
        public ICollection<BulletEffect> ListOfEffects { get; set; }

        public Vector2 directionOfMoving;

        //public Enemy target;
        public float speedOfMoving;

        //start moving to enemy
        public virtual void DoShot(Enemy target)
        {
            DoShot(target.transform.position - transform.position);
        }
        public virtual void DoShot(Vector2 direction)
        {
            directionOfMoving = direction;
            directionOfMoving.Normalize();
            Move();
        }
        //move according to enemy position and speed
        public abstract void Move();
        public abstract void SetPosition(Vector2 pos);
        public abstract void SetRotation(Quaternion rot);

        public virtual void OnFly(Vector2 positionOfFlying, Quaternion rotation)
        {
        }


        public virtual void Delete()
        {
            gameObject.SetActive(false);
            //Destroy(this.gameObject);
        }

        public bool destroyOnCollision;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (OnlineManager.DoNotOwnCalculations)
                return;
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                foreach(var ef in ListOfEffects)
                {
                    enemy.RecieveEffect(ef);
                }
                if (destroyOnCollision)
                {
                    Delete();
                }
                //ListOfEffects.ForEach(el => enemy.RecieveEffect(el));
            }
        }

        public abstract void Clone(Bullet t);
    }
}
