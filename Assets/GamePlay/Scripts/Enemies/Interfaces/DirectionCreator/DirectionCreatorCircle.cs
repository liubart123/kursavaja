using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.GamePlay.Scripts.Enemies.Interfaces.DirectionCreator
{
    public class DirectionCreatorCircle : DirectionCreator
    {
        private float currentAngle = 0;
        public float deltaAngle;


        public override Vector2 CreateDirection(DirectionCreatorParameters args)
        {
            currentAngle += deltaAngle;
            return new Vector2(Mathf.Cos(currentAngle), Mathf.Sin(currentAngle));
        }
    }
}
