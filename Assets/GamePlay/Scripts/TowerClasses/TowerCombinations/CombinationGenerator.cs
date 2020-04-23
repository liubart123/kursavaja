using Assets.GamePlay.Scripts.BulletEffects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.GamePlay.Scripts.TowerClasses.TowerCombinations
{
    // для генерацыі камбінацый з клясаў. У кожнага гульца свой экзэмпляр гэтага кляса
    public class CombinationGenerator : MonoBehaviour
    {
        List<TowerCombination> possibleCombinations = new List<TowerCombination>();

        private void Start()
        {
        }
        private void CreatePossibleCombinations()
        {

        }
        //стварыць магчымыя камбінацыі
        protected void CreateStartCombinations()
        {
            Player.Player owner = GetComponent<Player.Player>();
            TowerCombination tc = new TowerCombination(1);
            //tc.towerClasses = new List<TowerClass>
            //    {
            //        owner.towerClassCollection.GetTowerClass(typeof(DamageTowerClassBlue)),
            //        owner.towerClassCollection.GetTowerClass(typeof(PeriodicTowerClassBlue))
            //    };
            //tc.BulletEffects.Add(new BulletEffectImmidiateDamageRaw(1500,Damage.DamageManager.EKindOfDamage.red));
            //possibleCombinations.Add(tc);


            //tc = new TowerCombination(1);
            //tc.towerClasses = new List<TowerClass>
            //    {
            //        owner.towerClassCollection.GetTowerClass(typeof(DamageTowerClassBlue)),
            //        owner.towerClassCollection.GetTowerClass(typeof(TowerClassRaw4))
            //    };
            //tc.BulletEffects.Add(new BulletEffectPeriodicDamageRaw(5,Damage.DamageManager.EKindOfDamage.red));
            //possibleCombinations.Add(tc);
        }
        public void Initialize()
        {

            CreateStartCombinations();
        }

        public ICollection<TowerCombination> ConvertClassesToCombination(ICollection<TowerClass> classes)
        {
            List<TowerCombination> resultCombs = new List<TowerCombination>();
            foreach (var comb in possibleCombinations)
            {
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
