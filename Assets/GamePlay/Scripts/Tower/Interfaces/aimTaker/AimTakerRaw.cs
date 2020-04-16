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
                } else if (lastTarget != null && lastTarget != Vector2.zero)
                {
                    return lastTarget;
                }
            }
            var CurrentTarget = args.target;
            Vector2 tarPos = args.target.GetPosition();
            Vector2 tarDir = args.target.GetCurrentDirectionOfMoving();
            Vector2 tarSpeed = args.target.speed * tarDir;
            float bulletSpeed = args.BulletSpeed;
            if (args.target.speed == 0 || tarDir == Vector2.zero)
            {
                return -args.position + tarPos;
            }
            //Vector2 target = ((Vector2)CurrentTarget.gameObject.transform.position - args.position);
            Vector2 target;
            tarPos -= args.position;    //changing absolute target position to relative according to tower position
            float D; //discriminint
            float a = Mathf.Pow(tarSpeed.x, 2) + Mathf.Pow(tarSpeed.y, 2) - Mathf.Pow(bulletSpeed, 2);
            float b = 2 * (tarPos.x * tarSpeed.x + tarPos.y * tarSpeed.y);
            float c = Mathf.Pow(tarPos.x, 2) + Mathf.Pow(tarPos.y, 2);
            D = b * b - 4 * a * c;
            float t;
            t = -Mathf.Sqrt(D) - b;
            t /= a * 2;
            target = tarPos + tarSpeed * t;
            //target += args.position;

            //target = tarPos;

            lastTarget = target;
            return target;
        }
        public override void ResetAimTaker()
        {
            lastTarget = Vector2.zero;
        }

    }
}
