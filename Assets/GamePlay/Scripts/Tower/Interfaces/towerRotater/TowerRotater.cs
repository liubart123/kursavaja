using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.GamePlay.Scripts.Tower.Interfaces
{
    public abstract class TowerRotater : MonoBehaviour
    {
        public int MINIMUM_DELTA_ANGLE = 2;
        public float rotatingSpeed;
        public abstract bool RotateTower(TowerRotaterParameters args);
    }
    public class TowerRotaterParameters
    {
        public Vector2 direction;
        public Transform towerTransform;
        //public Action<float> RotateDelegate;
        public TowerRotaterParameters(Vector2 direction, Transform towerTransform)
        {
            this.direction = direction;
            this.towerTransform = towerTransform;
        }
    }
}
