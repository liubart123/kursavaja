using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.GamePlay.Scripts.Enemies.Interfaces.DirectionCreator
{
    public class DirectionCreatorRaw : DirectionCreator
    {
        public int maxCycleCount;
        private int cycleCounter = 0;
        [SerializeField]
        protected Vector2 directionFromUnity;

        public override Vector2 CreateDirection(DirectionCreatorParameters args)
        {
            if (cycleCounter++ > maxCycleCount)
            {
                cycleCounter = 0;
                directionFromUnity = -directionFromUnity;
            }
            return directionFromUnity.normalized;
        }
    }
}
