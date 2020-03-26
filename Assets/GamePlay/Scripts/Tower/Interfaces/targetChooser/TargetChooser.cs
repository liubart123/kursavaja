using Assets.GamePlay.Scripts.Enemies;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.GamePlay.Scripts.Tower.Interfaces
{
    public abstract class TargetChooser : MonoBehaviour
    {
        //chose target for tower from enemies in range 
        public abstract Enemy ChooseTarget(TargetChooserParameters args);
    }
    public class TargetChooserParameters
    {
        //public List<Enemy> possibleTargets;
        public float towerRotation;

        public TargetChooserParameters(float towerRotation)
        {
            this.towerRotation = towerRotation;
        }

        //public TargetChooserParameters(List<Enemy> possibleTargets, Vector3 towerRotation)
        //{
        //    this.possibleTargets = possibleTargets;
        //    this.towerRotation = towerRotation;
        //}
    }
}
