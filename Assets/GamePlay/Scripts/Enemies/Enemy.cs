using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.GamePlay.Scripts.Enemies
{
    public abstract class Enemy : MonoBehaviour
    {
        
        public Action Move { get; protected set; }
        public Action CreateTargetForMoving { get; protected set; }
        public Action CreateDirectionForMoving { get; protected set; }
        public Action<Vector2> SetPosition { get; protected set; }
        public Action<Vector2> SetRotation { get; protected set; }
        public void Update()
        {
            CreateDirectionForMoving();
            Move();
        }
    }
}
