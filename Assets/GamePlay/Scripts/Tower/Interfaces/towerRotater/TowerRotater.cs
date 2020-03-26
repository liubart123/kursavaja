﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.GamePlay.Scripts.Tower.Interfaces
{
    public abstract class TowerRotater : MonoBehaviour
    {
        public abstract bool RotateTower(TowerRotaterParameters args);
    }
    public class TowerRotaterParameters
    {
        public Vector3 direction;
        public Transform towerTransform;
        //public Action<float> RotateDelegate;
        public TowerRotaterParameters(Vector3 direction, Transform towerTransform)
        {
            this.direction = direction;
            this.towerTransform = towerTransform;
        }
    }
}
