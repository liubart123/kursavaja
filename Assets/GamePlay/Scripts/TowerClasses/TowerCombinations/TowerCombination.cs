using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.GamePlay.Scripts.TowerClasses.TowerCombinations
{
    public class TowerCombination : TowerClass
    {
        //collection of classes that must be gathered to creaete combination
        public ICollection<TowerClass> towerClasses;    
    }
}
