using Assets.GamePlay.Scripts.BulletEffects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Assets.GamePlay.Scripts.TowerClasses.TowerClasseGenerator;

namespace Assets.GamePlay.Scripts.TowerClasses
{
    public class TowerClass
    {
        public string Name { get; set; }
        public ETowerClass typeOfTower;
        public ICollection<BulletEffect> BulletEffects { get; protected set; } 
        public float effectivity = 1;
        public TowerClass(ETowerClass t, params BulletEffect[] effects)
        {
            BulletEffects = new List<BulletEffect>(effects);
            typeOfTower = t;
        }
    }
}
