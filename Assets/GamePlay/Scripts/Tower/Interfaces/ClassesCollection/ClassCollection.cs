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
        protected TowerClass defaultTowerClass; //special class for every kind of tower
        protected TowerClass bonusTowerClass;     //class that is got from bonuses
        protected ICollection<TowerClass> otherTowerClasses;    //classes that are got from other towers
        protected ICollection<TowerCombination> towerCombinations;    //generated combinations

        public abstract ICollection<TowerClass> GetAllClasses();
        public abstract ICollection<BulletEffect> GetAllEffects();
        public abstract void OnBonusTowerClassChange();
        public abstract void OnOtherTowersChange();
        public virtual void MakeInfluenceOnOtherTowers()
        {
            foreach(var t in GetTowersInRange())
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
        public virtual ICollection<Tower> GetTowersInRange()
        {

            int layerMask = 1 << 13;
            var hits = Physics2D.CircleCastAll(transform.position, influenceRange, Vector2.zero, 1, layerMask);
            Tower[] res = new Tower[hits.Length];
            for (int i = 0; i < hits.Length; i++)
            {
                res[i] = hits[i].collider.gameObject.GetComponent<Tower>();
            }
            return res;
        }
        public virtual void Die() { MakeInfluenceOnOtherTowers(); }
    }
}
