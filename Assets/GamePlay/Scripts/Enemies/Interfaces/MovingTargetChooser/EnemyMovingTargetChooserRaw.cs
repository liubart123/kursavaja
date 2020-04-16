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
        float searchRange = 0;
        [SerializeField]
        float deltaAngle;
        public override Building.Building ChooseTargetForMove(EnemyMovingTargetChooserParameters arg)
        {
            Building.Building result = null;

            float directionOfCircleCasting=0;
            while (directionOfCircleCasting<Mathf.PI*2)
            {
                int layerMask = 1 << 13;
                //RaycastHit2D hit = Physics2D.CircleCast(arg.enemyPos, searchRange, 
                //    new Vector2(Mathf.Cos(directionOfCircleCasting),
                //                Mathf.Sin(directionOfCircleCasting))

                //      , Mathf.Infinity, layerMask);
                RaycastHit2D hit = Physics2D.CircleCast(arg.enemyPos, searchRange, Vector2.zero, Mathf.Infinity, layerMask);
                if (hit.collider != null)
                {
                    return hit.collider.GetComponent<Tower.Tower>();
                }else
                {
                    return null;
                }
                directionOfCircleCasting += deltaAngle;
            }

            return result;
        }
    }
}
