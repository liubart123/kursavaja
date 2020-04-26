using Assets.GamePlay.Scripts.BulletEffects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Assets.GamePlay.Scripts.TowerClasses.TowerClasseGenerator;

namespace Assets.GamePlay.Scripts.TowerClasses.TowerCombinations
{
    public class TowerCombination : TowerClass
    {
        //collection of classes that must be gathered to creaete combination
        public ICollection<TowerClass> towerClasses;

        public TowerCombination(string name, params BulletEffect[] effects) : base (ETowerClass.combination, name, effects)
        {
        }
    }
}
