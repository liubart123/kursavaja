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
    public abstract class Bullet : MonoBehaviour , IDeletable
    {
        public Vector2 directionOfMoving;

        //public Enemy target;
        public float speedOfMoving;
        public EffectDecorator decorator;

        //start moving to enemy
        public abstract void DoShot(Enemy target);
        //move according to enemy position and speed
        public abstract void Move();
        public abstract Vector2 CalculateNextPosition();
        public abstract void SetPosition(Vector2 pos);
        public abstract void SetRotation(Quaternion rot);

        public virtual void OnFly(Vector2 positionOfFlying, Quaternion rotation)
        {
            if (decorator != null)
                decorator.OnFly(positionOfFlying, rotation);
        }
        public virtual void OnCollision(Enemy target)
        {
            if (decorator != null)
                decorator.OnCollision(target);
        }
        public virtual void OnCollisionEve(Enemy target, Vector2 positionOfFlying)
        {
            if (decorator != null)
                decorator.OnCollisionEve(target, positionOfFlying);

        }

        //destroing of itself 
        public abstract void SelfDestroy();

        //public void Update()
        //{
        //    Move();
        //}

        public virtual void Delete()
        {
            Destroy(this.gameObject);
        }
    }
}
