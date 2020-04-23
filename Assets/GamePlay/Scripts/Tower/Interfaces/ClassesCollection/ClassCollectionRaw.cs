using Assets.GamePlay.Scripts.BulletEffects;
using Assets.GamePlay.Scripts.TowerClasses;
using Assets.GamePlay.Scripts.TowerClasses.TowerCombinations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.GamePlay.Scripts.Tower.Interfaces.ClassesCollection
{
    public class ClassCollectionRaw : ClassCollection
    {
        public int[] idOfTowerClasess;
        public override ICollection<TowerClass> GetAllClasses()
        {
            List<TowerClass> res = new List<TowerClass>();
            res.Add(defaultTowerClass);
            if (ownTowerClass != null)
            {
                res.Add(ownTowerClass);
            }
            foreach(var cl in otherTowerClasses)
            {
                res.Add(cl);
            }
            //foreach (var comb in towerCombinations)
            //{
            //    foreach(var cl in comb.towerClasses)
            //    {
            //        res.Add(cl);
            //    }
            //}
            return res;
        }

        //
        public override void OnOtherTowersChange()
        {
            throw new NotImplementedException();
        }
        public override void OnOwnTowerClassChange()
        {
            throw new NotImplementedException();
        }
        //змяніць камбінацыі тавэра
        protected void ChangeCombinations()
        {

            CombinationGenerator combinationGenerator = GetComponent<Tower>().owner.possibleCombinations;
            towerCombinations = combinationGenerator.ConvertClassesToCombination(GetAllClasses());
        }
        private void Start()
        {
        }
        protected void SetClasses()
        {
            TowerClasseGenerator towerClassCollection = GetComponent<Tower>().owner.towerClassCollection;
            defaultTowerClass = towerClassCollection.GetTowerClass(idOfTowerClasess[0]);
            //ownTowerClass = towerClassCollection.GetTowerClass(typeof(PeriodicTowerClassBlue));
            otherTowerClasses = new List<TowerClass>();

            for (int i=1;i< idOfTowerClasess.Length; i++)
            {
                towerClassCollection.GetTowerClass(idOfTowerClasess[i]);

            }

            //otherTowerClasses.Add(towerClassCollection.GetTowerClass(typeof(TowerClassRaw4)));
            //otherTowerClasses.Add(towerClassCollection.GetTowerClass(typeof(TowerClassRaw2)));
            //otherTowerClasses.Add(towerClassCollection.GetTowerClass(typeof(TowerClassRaw3)));
            towerCombinations = new List<TowerCombination>();
        }

        public override void Initialize()
        {
            SetClasses();
            ChangeCombinations();
        }

        public override ICollection<BulletEffect> GetAllEffects()
        {
            List<BulletEffect> res = new List<BulletEffect>();
            var classes = GetAllClasses();
            foreach (var cl in classes)
            {
                foreach (var eff in cl.BulletEffects)
                {
                    res.Add(eff.Clone());
                }
            }
            foreach (var cl in towerCombinations)
            {
                foreach (var eff in cl.BulletEffects)
                {
                    res.Add(eff.Clone());
                }
            }
            return res;
        }
    }
}
