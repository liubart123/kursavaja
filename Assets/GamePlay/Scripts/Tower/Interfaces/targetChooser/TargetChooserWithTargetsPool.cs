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
                pool.TargetsInRange.ForEach(el =>
                {
                    //Debug.Log("direction of shooting " + args.towerPosition + ". Current direction: " + towerDirection);
                    float tempAngle = Vector2.Angle(towerDirection, -args.towerPosition + (Vector2)el.GetPosition());
                    tempAngle /= Mathf.PI;

                    float distance = Vector2.Distance(args.towerPosition, el.GetPosition());
                    distance /= pool.towerRange;

                    currentCost = tempAngle / towerRotater.rotatingSpeed + distance;
                    if (currentCost < minCost)
                    {
                        minCost = currentCost;
                        res = el;
                    }
                });
                if (res != null)
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
