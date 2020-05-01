using Assets.GamePlay.Scripts.BulletEffects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static Assets.GamePlay.Scripts.TowerClasses.TowerClasseGenerator;

namespace Assets.GamePlay.Scripts.TowerClasses.TowerCombinations
{
    public class TowerCombination : TowerClass
    {
        //collection of classes that must be gathered to creaete combination
        public ICollection<TowerClass> towerClasses;

        public TowerCombination(string name, ICollection<TowerClass> towerClasses, ETypeOfCombination type, Color color, params BulletEffect[] effects) : base (ETowerClass.combination, name, effects)
        {
            this.towerClasses = towerClasses;
            this.TowerClassColor = color;
            typeOfCombination = type;

        }
        public ETypeOfCombination typeOfCombination;
        public enum ETypeOfCombination
        {
            i,ii,iii,iv
        }
    }
}
