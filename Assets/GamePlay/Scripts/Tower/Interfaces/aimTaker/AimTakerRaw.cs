using Assets.GamePlay.Scripts.Enemies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.GamePlay.Scripts.Tower.Interfaces.aimRotater
{
    public class AimTakerRaw : AimTaker
    {
        [SerializeField]
        protected int maxCycleCountToForCalculating;
        protected int cycleCount = 0;
        protected Vector2 lastTarget;
        public override Vector2 TakeAim(AimTakerParameters args)
        {
            if (args.getActualValue == false)
            {
                if (cycleCount++ > maxCycleCountToForCalculating)
                {
                    cycleCount = 0;
                } else if (lastTarget != null)
                {
                    return lastTarget;
                }
            }
            var CurrentTarget = args.target;
            Vector2 tarPos = args.target.GetPosition();
            Vector2 tarDir = args.target.GetCurrentDirectionOfMoving();
            Vector2 tarSpeed = args.target.speed * tarDir;
            float bulletSpeed = args.BulletSpeed;
            //Vector2 target = ((Vector2)CurrentTarget.gameObject.transform.position - args.position);
            Vector2 target;
            tarPos -= args.position;    //changing target position to relative according to tower position
            float D; //discriminint
            D = Mathf.Pow(2 * (tarSpeed.x * tarPos.x + tarSpeed.y * tarPos.y), 2);
            float a = Mathf.Pow(tarSpeed.x,2) + Mathf.Pow(tarSpeed.y, 2) - Mathf.Pow(bulletSpeed, 2);
            D -= 4 * (a) * (Mathf.Pow(tarPos.x, 2) + Mathf.Pow(tarPos.y, 2));
            float t = - Mathf.Sqrt(D) - 2 * (tarSpeed.x * tarPos.x + tarSpeed.y * tarPos.y);
            t /= 2 * a;
            target = new Vector2(tarSpeed.x * t + args.target.GetPosition().x,
                tarSpeed.y * t + args.target.GetPosition().y);
            lastTarget = target;
            return target;
        }

    }
}
