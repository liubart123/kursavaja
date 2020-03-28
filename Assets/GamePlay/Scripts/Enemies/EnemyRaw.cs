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

        protected Vector2 currentTarget;
        protected Vector2 currentDirection;
        public float speed;
        public void MoveRaw()
        {
            SetPosition((Vector2)transform.position + currentDirection * speed);
        }
        public void SetPositionRaw(Vector2 pos)
        {
            transform.position = pos;
        }
        public void SetRotationRaw(Vector2 pos)
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
            currentDirection = new Vector2(Mathf.Cos(angleOfMoving), Mathf.Sin(angleOfMoving));
        }
    }
}
