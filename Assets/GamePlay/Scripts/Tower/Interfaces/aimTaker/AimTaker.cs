using Assets.GamePlay.Scripts.Enemies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.GamePlay.Scripts.Tower.Interfaces
{
    public abstract class AimTaker : MonoBehaviour
    {
        public abstract Vector2 TakeAim(AimTakerParameters args);
        public float effectivity=1;
        public virtual void ResetAimTaker()
        {

        }
    }
    public class AimTakerParameters
    {
        public Enemy target;
        public Vector2 position;
        public float BulletSpeed;
        public bool getActualValue;

        public AimTakerParameters(Enemy target, Vector2 position, float BulletSpeed, bool getActualValue = false)
        {
            this.target = target;
            this.position = position;
            this.BulletSpeed = BulletSpeed;
            this.getActualValue = getActualValue;
        }
    }
}
