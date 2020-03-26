using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.GamePlay.Scripts.Enemies
{
    public class EnemyRaw : Enemy
    {
        public EnemyRaw()
        {
            Move = MoveRaw;
            CreateDirectionForMoving = CreateDirectionForMovingRaw;
            SetPosition = SetPositionRaw;
        }

        protected Vector3 currentTarget;
        protected Vector3 currentDirection;
        public float speed;
        public void MoveRaw()
        {
            SetPosition(transform.position + currentDirection * speed);
        }
        public void SetPositionRaw(Vector3 pos)
        {
            transform.position = pos;
        }
        public void SetRotationRaw(Vector3 pos)
        {
            //transform.rotation = Quaternion;
        }
        public void CreateTargetForMovingRaw()
        {

        }
        public float angleOfMoving;
        public float deltaAngle;
        public void CreateDirectionForMovingRaw()
        {
            angleOfMoving += deltaAngle;
            currentDirection = new Vector3(Mathf.Cos(angleOfMoving), Mathf.Sin(angleOfMoving), 0);
        }
    }
}
