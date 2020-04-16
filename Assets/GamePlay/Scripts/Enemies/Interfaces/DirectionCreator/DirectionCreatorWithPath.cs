using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.GamePlay.Scripts.Enemies.Interfaces.DirectionCreator
{
    public class DirectionCreatorWithPath : DirectionCreator
    {
        public float minDistanceForMoving;
        protected Vector2 pastDirection = Vector2.zero;
        public override Vector2 CreateDirection(DirectionCreatorParameters args)
        {
            if (args.path == null || args.path.Count==0)
            {
                return Vector2.zero;
            }
            Block next = args.path.Last();
            if (next != null)
            {
                if (Vector2.Distance(next.GetPosition(), args.enemyPos) < minDistanceForMoving)
                {
                    args.path.Remove(next);
                    if (args.path.Count != 0)
                    {
                        return (args.path.Last().GetPosition() - args.enemyPos).normalized;
                    } else
                    {
                        return Vector2.zero;
                    }
                } else
                {
                    return (next.GetPosition() - args.enemyPos).normalized;
                }
            }
            return Vector2.zero;
        }
    }
}
