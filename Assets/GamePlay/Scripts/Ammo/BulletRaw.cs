using Assets.GamePlay.Scripts.Enemies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.GamePlay.Scripts.Ammo
{
    public class BulletRaw : Bullet
    {
        public BulletRaw()
        {
            DoShot = DoShotRaw;
            Move = MoveRaw;
            SetPosition = SetPositionRaw;
            SetRotation = SetRotationRaw;
            SelfDestroy = SelfDestroyRaw;
            Live = LiveRaw;
            CalculateNextPosition = CalculateNextPositionRaw;
        }

        public void DoShotRaw(Enemy target) {
            directionOfMoving = target.transform.position - transform.position;
            directionOfMoving.z = 0;
            directionOfMoving.Normalize();
        }
        protected Vector3 directionOfMoving;
        public float speedofMoving;
        public void MoveRaw()
        {
            SetPositionRaw(CalculateNextPosition());

        }
        public Vector3 CalculateNextPositionRaw()
        {
            return directionOfMoving * speedofMoving + transform.position;
        }
        public void SetPositionRaw(Vector3 pos)
        {
            transform.position = pos;
        }
        public void SetRotationRaw(Quaternion pos)
        {
            transform.rotation = pos;
        }
        public void SelfDestroyRaw()
        {
            Destroy(this.gameObject);
        }
        //count of frames for living
        public const int timeOfLive = 100;
        protected int currentTimeOfLive = 0;
        public void LiveRaw()
        {
            if (currentTimeOfLive++ > timeOfLive)
            {
                SelfDestroy();
            }
        }
    }
}
