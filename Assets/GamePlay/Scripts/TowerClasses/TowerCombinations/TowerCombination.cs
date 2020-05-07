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
        public ICollection<ETowerClass> towerClasses;

        public TowerCombination(string name, ICollection<ETowerClass> towerClasses, ETypeOfCombination type, Color color, params BulletEffect[] effects) : base (ETowerClass.combination, name, effects)
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

        public void Deserialize(TowerCombinationSer ser)
        {
            towerClasses = new List<ETowerClass>();
            foreach(var clas in ser.towerClasses)
            {
                towerClasses.Add((ETowerClass)clas);
            }
            typeOfCombination = (ETypeOfCombination)ser.typeOfCombination;
            bulletEffects = ser.effects;
        }
    }

    [Serializable]
    public class TowerCombinationSer
    {
        [SerializeReference]
        public List<int> towerClasses;
        public int typeOfCombination;
        [SerializeReference]
        public List<BulletEffect> effects;
        public TowerCombinationSer()
        {

        }
        public TowerCombinationSer(TowerCombination comb)
        {
            towerClasses = comb.towerClasses.Select(el => (int)el).ToList();
            typeOfCombination = (int)comb.typeOfCombination;
            effects = comb.bulletEffects.ToList();
        }
    }
}
