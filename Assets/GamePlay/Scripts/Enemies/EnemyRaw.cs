using Assets.GamePlay.Scripts.Enemies.Interfaces.DirectionCreator;
using Assets.GamePlay.Scripts.Enemies.Interfaces.MovingTargetChooser;
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
        protected Rigidbody2D rigidBody;
        public override void Move()
        {
            rigidBody.velocity = currentDirection * speed; 
            
        }
        protected override void Start()
        {
            //Initialize();
        }
        public override void Initialize()
        {
            rigidBody = GetComponent<Rigidbody2D>();
            base.Initialize();

        }
    }
}
