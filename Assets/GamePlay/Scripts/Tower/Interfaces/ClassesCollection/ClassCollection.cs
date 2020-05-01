using Assets.GamePlay.Scripts.BulletEffects;
using Assets.GamePlay.Scripts.TowerClasses;
using Assets.GamePlay.Scripts.TowerClasses.TowerCombinations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.GamePlay.Scripts.Tower.Interfaces.ClassesCollection
{
    public abstract class ClassCollection : MonoBehaviour
    {
        //TOWER_CLASSES
        [SerializeField]
        protected int influenceRange;
        public TowerClass defaultTowerClass; //special class for every kind of tower
        public TowerClass bonusTowerClass;     //class that is got from bonuses
        protected ICollection<TowerClass> otherTowerClasses;    //classes that are got from other towers
        protected ICollection<TowerCombination> towerCombinations;    //generated combinations

        public abstract ICollection<TowerClass> GetAllClasses();
        public abstract ICollection<BulletEffect> GetAllEffects();
        public virtual ICollection<TowerCombination> GetAllCombinations()
        {
            return towerCombinations;
        }
        
        public abstract void OnBonusTowerClassChange();//змяненне канвеера бонусаў
        
        public abstract void OnOtherTowersChange();//змяненне бліжайшых тавэроў
        public virtual void MakeInfluenceOnOtherTowers(bool isThisTowerDead = false)
        {
            var towers = GetTowersInRange(isThisTowerDead);
            if (towers == null)
                return;
            foreach (var t in towers)
            {
                t.GetComponent<Tower>().classCollection.OnOtherTowersChange();
            }
        }

        public abstract void Initialize();
        public virtual ICollection<Block> GetBlocksInRange()
        {
            int layerMask = 1 << 10;
            var hits = Physics2D.CircleCastAll(transform.position, influenceRange, Vector2.zero, 1, layerMask);
            Block[] res = new Block[hits.Length];
            for (int i = 0; i < hits.Length; i++)
            {
                res[i] = hits[i].collider.gameObject.GetComponent<Block>();
            }
            return res;
        }
        public virtual ICollection<Tower> GetTowersInRange(bool isThisTowerDead = false)
        {

            int layerMask = 1 << 13;
            var hits = Physics2D.CircleCastAll(transform.position, influenceRange, Vector2.zero, 1, layerMask);

            Tower[] res;
            if (isThisTowerDead)
            {
                res = new Tower[hits.Length];
                for (int i = 0; i < hits.Length; i++)
                {
                    res[i] = hits[i].collider.gameObject.GetComponent<Tower>();
                }
            }else
            {
                res = new Tower[hits.Length - 1];
                bool wasThisTower = false;
                for (int i = 0; i < hits.Length; i++)
                {
                    if (hits[i].collider.gameObject.GetComponent<Tower>().classCollection == this)
                    {
                        wasThisTower = true;
                        continue;
                    }
                    res[i - (wasThisTower ? 1 : 0)] = hits[i].collider.gameObject.GetComponent<Tower>();
                }
            }
            return res;
        }
        public virtual void Die() { MakeInfluenceOnOtherTowers(true); }
    }
}
