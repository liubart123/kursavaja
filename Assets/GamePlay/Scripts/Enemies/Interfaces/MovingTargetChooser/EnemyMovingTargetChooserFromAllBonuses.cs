using Assets.GamePlay.Scripts.Bonuses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.GamePlay.Scripts.Enemies.Interfaces.MovingTargetChooser
{
    public class EnemyMovingTargetChooserFromAllBonuses : EnemyMovingTargetChooser
    {
        public static ICollection<Bonus> bonuses;

        public override Building.Building ChooseTargetForMove(EnemyMovingTargetChooserParameters arg)
        {
            Building.Building result = null;
            if (bonuses == null) { bonuses = FindObjectsOfType<Bonuses.Bonus>(); }
            //var bonuses = FindObjectsOfType<Bonuses.Bonus>();
            float minDistance = Mathf.Infinity;
            foreach(var b in bonuses)
            {
                var dist = Vector2.Distance(b.transform.position, arg.enemyPos);
                if (dist < minDistance)
                {
                    minDistance = dist;
                    result = b;
                }
            }

            return result;
        }

        public override void Reset()
        {
            bonuses = null;
            base.Reset();
        }
    }
}
