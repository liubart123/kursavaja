using Assets.GamePlay.Scripts.BulletEffects;
using Assets.GamePlay.Scripts.TowerClasses;
using Assets.GamePlay.Scripts.TowerClasses.TowerCombinations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.GamePlay.Scripts.Tower.Interfaces.ClassesCollection
{
    public abstract class ClassCollection : MonoBehaviour
    {
        //TOWER_CLASSES
        [SerializeField]
        protected int influenceRange;
        protected TowerClass defaultTowerClass; //special class for every kind of tower
        protected TowerClass bonusTowerClass;     //class that is got from bonuses
        protected ICollection<TowerClass> otherTowerClasses;    //classes that are got from other towers
        protected ICollection<TowerCombination> towerCombinations;    //generated combinations

        public abstract ICollection<TowerClass> GetAllClasses();
        public abstract ICollection<BulletEffect> GetAllEffects();
        public abstract void OnBonusTowerClassChange();
        public abstract void OnOtherTowersChange();

        public abstract void Initialize();
    }
}
