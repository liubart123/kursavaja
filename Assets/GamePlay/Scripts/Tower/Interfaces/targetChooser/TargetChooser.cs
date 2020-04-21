using Assets.GamePlay.Scripts.Enemies;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.GamePlay.Scripts.Tower.Interfaces
{
    public abstract class TargetChooser : MonoBehaviour
    {
        //chose target for tower from enemies in range 
        public abstract Enemy ChooseTarget(TargetChooserParameters args);
        public virtual void Initialize()
        {

        }
    }
    public class TargetChooserParameters
    {
        //public List<Enemy> possibleTargets;
        public float towerRotation;
        public Vector2 towerPosition;

        public TargetChooserParameters(float towerRotation)
        {
            this.towerRotation = towerRotation;
        }

        public TargetChooserParameters(float towerRotation, Vector2 towerPosition) : this(towerRotation)
        {
            this.towerPosition = towerPosition;
        }

        //public TargetChooserParameters(List<Enemy> possibleTargets, Vector2 towerRotation)
        //{
        //    this.possibleTargets = possibleTargets;
        //    this.towerRotation = towerRotation;
        //}
    }
    
}
