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
        public abstract Vector3 TakeAim(AimTakerParameters args);
    }
    public class AimTakerParameters
    {
        public Enemy target;
        public Vector3 position;

        public AimTakerParameters(Enemy target, Vector3 position)
        {
            this.target = target;
            this.position = position;
        }
    }
}
