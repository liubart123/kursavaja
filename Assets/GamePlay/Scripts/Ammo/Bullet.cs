using Assets.GamePlay.Scripts.Enemies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.GamePlay.Scripts.Ammo
{
    public abstract class Bullet : MonoBehaviour
    {
        public Vector3 directionOfMoving;
        public float speedofMoving;
        public EffectDecorator decorator;

        //start moving to enemy
        public abstract void DoShot(Enemy target);
        //move according to enemy position and speed
        public abstract void Move();
        public abstract Vector3 CalculateNextPosition();
        public abstract void SetPosition(Vector3 pos);
        public abstract void SetRotation(Quaternion rot);

        public virtual void OnFly(Vector3 positionOfFlying, Quaternion rotation)
        {
            if (decorator != null)
                decorator.OnFly(positionOfFlying, rotation);
        }
        public virtual void OnCollision(Enemy target)
        {
            if (decorator != null)
                decorator.OnCollision(target);
        }
        public virtual void OnCollisionEve(Enemy target, Vector3 positionOfFlying)
        {
            if (decorator != null)
                decorator.OnCollisionEve(target, positionOfFlying);

        }

        //destroing of itself 
        public abstract void SelfDestroy();

        public void Update()
        {
            Move();
        }
    }
}
