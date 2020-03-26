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
        //start moving to enemy
        public Action<Enemy> DoShot { get; protected set; }
        //move according to enemy position and speed
        public Action Move { get; protected set; }
        public Func<Vector3> CalculateNextPosition { get; protected set; }
        public Action<Vector3> SetPosition { get; protected set; }
        public Action<Quaternion> SetRotation { get; protected set; }
        //destroing of itself 
        public Action SelfDestroy { get; protected set; }
        //method for controll time of living of bullet
        public Action Live { get; protected set; }

        public void Update()
        {
            Move();
            Live();
        }
    }
}
