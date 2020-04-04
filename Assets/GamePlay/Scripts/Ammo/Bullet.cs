using Assets.GamePlay.Scripts.BulletEffects;
using Assets.GamePlay.Scripts.Enemies;
using Assets.GamePlay.Scripts.Other.TimeForLiving;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.GamePlay.Scripts.Ammo
{
    public abstract class Bullet : MonoBehaviour, IDeletable
    {
        public List<BulletEffect> ListOfEffects { get; set; }

        public Vector2 directionOfMoving;

        //public Enemy target;
        public float speedOfMoving;

        //start moving to enemy
        public virtual void DoShot(Enemy target)
        {
            DoShot(target.transform.position - transform.position);
        }
        public abstract void DoShot(Vector2 direction);
        //move according to enemy position and speed
        public abstract void Move();
        public abstract void SetPosition(Vector2 pos);
        public abstract void SetRotation(Quaternion rot);

        public virtual void OnFly(Vector2 positionOfFlying, Quaternion rotation)
        {
        }
        public virtual void OnCollision(Enemy target)
        {
            ListOfEffects.ForEach(el => el.AffectOnce(target));
        }
        public virtual void OnCollisionEve(Enemy target, Vector2 positionOfFlying)
        {

        }


        public virtual void Delete()
        {
            //Destroy(this.gameObject);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                ListOfEffects.ForEach(el => enemy.RecieveEffect(el));
            }
        }
    }
}
