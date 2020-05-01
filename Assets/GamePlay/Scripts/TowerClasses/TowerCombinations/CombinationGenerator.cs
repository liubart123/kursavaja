using Assets.GamePlay.Scripts.BulletEffects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static Assets.GamePlay.Scripts.Damage.DamageManager;
using static Assets.GamePlay.Scripts.TowerClasses.TowerCombinations.TowerCombination;

namespace Assets.GamePlay.Scripts.TowerClasses.TowerCombinations
{
    // для генерацыі камбінацый з клясаў. У кожнага гульца свой экзэмпляр гэтага кляса
    public class CombinationGenerator : MonoBehaviour
    {
        List<TowerCombination> possibleCombinations = new List<TowerCombination>();
        Player.Player owner;

        private float damage = 500;
        private float periodicDamage = 1;
        private float slowing = 1.5f;
        //стварыць магчымыя камбінацыі
        protected void CreateStartCombinations()
        {
            owner = GetComponent<Player.Player>();
            //var classes = new List<TowerClass>();
            //classes.Add(owner.towerClassCollection.GetTowerClass(TowerClasseGenerator.ETowerClass.damageGreen));
            //classes.Add(owner.towerClassCollection.GetTowerClass(TowerClasseGenerator.ETowerClass.damageRed));
            TowerCombination tc1 = new TowerCombination("I", new List<TowerClass>(), ETypeOfCombination.i, Color.cyan,
                new BulletEffectSlowingRaw(slowing));
            TowerCombination tc2 = new TowerCombination("II", new List<TowerClass>(), ETypeOfCombination.ii, Color.magenta,
                new BulletEffectImmidiateDamageRaw(damage,EKindOfDamage.blue));
            TowerCombination tc3 = new TowerCombination("III", new List<TowerClass>(), ETypeOfCombination.iii, Color.yellow,
                new BulletEffectPeriodicDamageRaw(periodicDamage,EKindOfDamage.green));
            TowerCombination tc4 = new TowerCombination("IV", new List<TowerClass>(), ETypeOfCombination.iv, Color.green,
                new BulletEffectPeriodicDamageRaw(periodicDamage, EKindOfDamage.red));

            possibleCombinations.Add(tc1);
            possibleCombinations.Add(tc2);
            possibleCombinations.Add(tc3);
            possibleCombinations.Add(tc4);
        }
        public void Initialize()
        {
            CreateStartCombinations();
        }
        public void Initialize(Player.Player pl)
        {
            owner = pl;
            Initialize();
        }
        public TowerCombination GetTowerCombination(ETypeOfCombination t)
        {
            return possibleCombinations.FirstOrDefault((el) => t == el.typeOfCombination);
        }
        public ICollection<TowerCombination> ConvertClassesToCombination(ICollection<TowerClass> classes)
        {
            List<TowerCombination> resultCombs = new List<TowerCombination>();
            foreach (var comb in possibleCombinations)
            {
                if (comb.towerClasses.Count == 0)
                {
                    continue;
                }
                bool res = true;
                foreach (var cl in comb.towerClasses)
                {
                    if (!classes.Contains(cl))
                    {
                        res = false;
                        break;
                    }
                }
                if (res)
                {
                    resultCombs.Add(comb);
                }
            }
            return resultCombs;
        }

    }
}
