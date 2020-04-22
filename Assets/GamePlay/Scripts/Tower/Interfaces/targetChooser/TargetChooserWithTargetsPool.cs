using Assets.GamePlay.Scripts.Enemies;
using Assets.GamePlay.Scripts.Tower.auxil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.GamePlay.Scripts.Tower.Interfaces.targetChooser
{
    public class TargetChooserWithTargetsPool : TargetChooser
    {
        [SerializeField]
        TargetPool pool;
        TowerRotater towerRotater;

        private object locker = new object();
        public override Enemy ChooseTarget(TargetChooserParameters args)
        {
            if (pool.TargetsInRange.Count == 0)
            {
                return null;
            } else
            {
                Vector2 towerDirection = new Vector2(
                    Mathf.Cos(args.towerRotation),
                    Mathf.Sin(args.towerRotation));
                float minCost = Mathf.Infinity;
                float currentCost;
                Enemy res = null;
                foreach (var el in pool.TargetsInRange)
                {

                    lock (locker)
                    {
                        if (el != null && el.gameObject.activeSelf)
                        {
                            //Debug.Log("direction of shooting " + args.towerPosition + ". Current direction: " + towerDirection);
                            float tempAngle = Vector2.Angle(towerDirection, -args.towerPosition + (Vector2)el.GetPosition());
                            tempAngle /= 180;

                            Vector2 enemyPos;
                            if (el == null)
                            {
                                continue;
                            }
                            enemyPos = el.GetPosition();
                            float distance = Vector2.Distance(args.towerPosition, enemyPos);
                            distance /= pool.towerRange;
                            distance *= 1;  //трэба зрабіць каб залежнасць ад дыстанцыі была большая

                            currentCost = tempAngle / towerRotater.rotatingSpeed + distance;
                            if (currentCost < minCost && el != null)
                            {
                                minCost = currentCost;
                                res = el;
                            }
                        }
                    }
                }
                if (res != null && res.gameObject.activeSelf)
                {
                    res.eventsWhenThisDie += (en) =>
                    {
                        pool.TargetsInRange.Remove(en);
                    };
                }
                return res;
            }
        }

        public override void Initialize()
        {
            towerRotater = GetComponent<TowerRotater>();
            base.Initialize();
        }
    }
}
