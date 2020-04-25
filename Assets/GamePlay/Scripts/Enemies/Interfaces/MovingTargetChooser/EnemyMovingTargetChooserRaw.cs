using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.GamePlay.Scripts.Enemies.Interfaces.MovingTargetChooser
{
    public class EnemyMovingTargetChooserRaw : EnemyMovingTargetChooser
    {
        [SerializeField]
        float searchRange = 10;
        [SerializeField]
        float deltaRange;
        public override Building.Building ChooseTargetForMove(EnemyMovingTargetChooserParameters arg)
        {
            Building.Building result = null;
            float currentSearchingRange = searchRange;

            while (currentSearchingRange < deltaRange*100)
            {
                int layerMask = 1 << 15;
                //RaycastHit2D hit = Physics2D.CircleCast(arg.enemyPos, searchRange, 
                //    new Vector2(Mathf.Cos(directionOfCircleCasting),
                //                Mathf.Sin(directionOfCircleCasting))

                //      , Mathf.Infinity, layerMask);
                RaycastHit2D hit = Physics2D.CircleCast(arg.enemyPos, currentSearchingRange, Vector2.zero, Mathf.Infinity, layerMask);
                if (hit.collider != null)
                {
                    result = hit.collider.GetComponent<Building.Building>();
                    break;
                }
                currentSearchingRange += deltaRange;
            }

            return result;
        }
    }
}
