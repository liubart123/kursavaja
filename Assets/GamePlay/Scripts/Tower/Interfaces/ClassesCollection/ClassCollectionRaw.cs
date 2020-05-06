using Assets.GamePlay.Scripts.BulletEffects;
using Assets.GamePlay.Scripts.TowerClasses;
using Assets.GamePlay.Scripts.TowerClasses.TowerCombinations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Assets.GamePlay.Scripts.TowerClasses.TowerClasseGenerator;

namespace Assets.GamePlay.Scripts.Tower.Interfaces.ClassesCollection
{
    public class ClassCollectionRaw : ClassCollection
    {
        protected TowerClasseGenerator classGenerator;
        public ETowerClass[] idOfTowerClasess;
        protected Tower tower;
        public override ICollection<TowerClass> GetAllClasses()
        {
            List<TowerClass> res = new List<TowerClass>();
            res.Add(defaultTowerClass);
            if (bonusTowerClass != null)
            {
                res.Add(bonusTowerClass);
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
            var towers = GetTowersInRange();
            otherTowerClasses.Clear();
            if (towers == null)
                return;
            foreach (var t in towers)
            {
                //клясы суседняга тавэра заносяцца сюды
                var classes = new List<TowerClass>();
                classes.Add(t.classCollection.defaultTowerClass);
                if (t.classCollection.bonusTowerClass != null)
                {
                    classes.Add(t.classCollection.bonusTowerClass);
                }
                //і надаюцца гэтаму тавэру, улічваючы паўторы
                foreach (var cl in classes)
                {
                    if (!otherTowerClasses.Contains(cl))
                    {
                        otherTowerClasses.Add(cl);
                    }
                }
            }

            //у клясах, атрыманых з суседніх тавэроў не можа быць
            //дэфолтнага кляса гэтага тавэра, альбо кляса, атрыманага з канвеера
            if (otherTowerClasses.Contains(defaultTowerClass))
            {
                otherTowerClasses.Remove(defaultTowerClass);
            }
            if (otherTowerClasses.Contains(bonusTowerClass))
            {
                otherTowerClasses.Remove(bonusTowerClass);
            }
            ChangeCombinations();
        }
        public override void OnBonusTowerClassChange()
        {
            bonusTowerClass = classGenerator.GetClassFromBonuses(tower.bonusConveyor);
            MakeInfluenceOnOtherTowers();
            ChangeCombinations();
        }

        protected CombinationGenerator combinationGenerator;
        //змяніць камбінацыі тавэра
        public override void ChangeCombinations()
        {
            combinationGenerator = GetComponent<Tower>().owner.possibleCombinations;
            towerCombinations = combinationGenerator.ConvertClassesToCombination(GetAllClasses());
        }
        private void Start()
        {
        }
        //set default tower class
        protected void SetClasses()
        {
            defaultTowerClass = classGenerator.GetTowerClass(idOfTowerClasess[0]);
            //ownTowerClass = towerClassCollection.GetTowerClass(typeof(PeriodicTowerClassBlue));
            otherTowerClasses = new List<TowerClass>();

            //for (int i=1;i< idOfTowerClasess.Length; i++)
            //{
            //    classGenerator.GetTowerClass(idOfTowerClasess[i]);

            //}

            //otherTowerClasses.Add(towerClassCollection.GetTowerClass(typeof(TowerClassRaw4)));
            //otherTowerClasses.Add(towerClassCollection.GetTowerClass(typeof(TowerClassRaw2)));
            //otherTowerClasses.Add(towerClassCollection.GetTowerClass(typeof(TowerClassRaw3)));
            towerCombinations = new List<TowerCombination>();
        }

        public override void Initialize()
        {
            tower = GetComponent<Tower>();
            classGenerator = tower.owner.towerClassCollection;
            SetClasses();
            OnOtherTowersChange();
            MakeInfluenceOnOtherTowers();
            ChangeCombinations();
        }

        public override ICollection<BulletEffect> GetAllEffects()
        {
            List<BulletEffect> res = new List<BulletEffect>();
            var classes = GetAllClasses();
            foreach (var cl in classes)
            {
                foreach (var eff in cl.bulletEffects)
                {
                    res.Add(eff.Clone());
                }
            }
            foreach (var cl in towerCombinations)
            {
                foreach (var eff in cl.bulletEffects)
                {
                    res.Add(eff.Clone());
                }
            }
            return res;
        }
    }
}
