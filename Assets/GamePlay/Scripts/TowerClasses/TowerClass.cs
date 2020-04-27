using Assets.GamePlay.Scripts.Bonuses;
using Assets.GamePlay.Scripts.BulletEffects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static Assets.GamePlay.Scripts.Bonuses.Bonus;
using static Assets.GamePlay.Scripts.TowerClasses.TowerClasseGenerator;

namespace Assets.GamePlay.Scripts.TowerClasses
{
    public class TowerClass
    {
        public string TowerClassName { get; set; }
        public Color TowerClassColor { get; set; }
        public ETowerClass typeOfTower;
        public ICollection<BulletEffect> BulletEffects { get; protected set; } 
        public float effectivity = 1;
        public EBonusType[] bonusesForClass;
        public TowerClass(ETowerClass t, string className, params BulletEffect[] effects)
        {
            BulletEffects = new List<BulletEffect>(effects);
            typeOfTower = t;
            TowerClassName = className;
        }
        public TowerClass(ETowerClass t, string className, EBonusType[] bonuses, params BulletEffect[] effects) 
        {
            BulletEffects = new List<BulletEffect>(effects);
            typeOfTower = t;
            TowerClassName = className;
            bonusesForClass = bonuses;
        }
        public TowerClass(ETowerClass t, string className, Color color, EBonusType[] bonuses, params BulletEffect[] effects)
        {
            BulletEffects = new List<BulletEffect>(effects);
            typeOfTower = t;
            TowerClassName = className;
            bonusesForClass = bonuses;
            TowerClassColor = color;
        }
    }
}
