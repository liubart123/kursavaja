using Assets.GamePlay.Scripts.Enemies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.GamePlay.Scripts.Tower.auxil
{
    public class TargetPool : MonoBehaviour
    {
        [SerializeField]
        protected Tower tower;
        public float towerRange;
        public List<Enemy> TargetsInRange { get; protected set; }
        protected virtual void Start()
        {
            TargetsInRange = new List<Enemy>();
        }
        protected virtual void OnTriggerEnter2D(Collider2D collision)
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            if (enemy != null && !TargetsInRange.Contains(enemy))
            {
                TargetsInRange.Add(enemy);
                tower.ChooseTarget();
            }
        }
        protected virtual void OnTriggerExit2D(Collider2D collision)
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            if (enemy != null && TargetsInRange.Contains(enemy))
            {
                TargetsInRange.Remove(enemy);
                tower.ChooseTarget();
            }
        }

        public virtual void Initialize()
        {
            GetComponent<CircleCollider2D>().radius = towerRange;
        }
    }
}
