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
                float minAngle = Mathf.Infinity;
                Enemy res = null;
                pool.TargetsInRange.ForEach(el =>
                {
                    //Debug.Log("direction of shooting " + args.towerPosition + ". Current direction: " + towerDirection);
                    float tempAngle = Vector2.Angle(towerDirection, -args.towerPosition + (Vector2)el.GetPosition());
                    if (tempAngle < minAngle)
                    {
                        minAngle = tempAngle;
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
    }
}
