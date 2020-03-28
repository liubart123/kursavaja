using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.GamePlay.Scripts.Tower.Interfaces.towerRotater
{
    class TowerRotaterRaw : TowerRotater
    {
        public const int MINIMUM_DELTA_ANGLE = 2;
        public float rotatingSpeed;
        public override bool RotateTower(TowerRotaterParameters args)
        {
            Vector2 target = args.direction;
            float zAngle = args.towerTransform.eulerAngles.z + 90;
            zAngle *= Mathf.PI / 180;
            Vector2 rotation = new Vector2(Mathf.Cos(zAngle), Mathf.Sin(zAngle));
            float deltaAngle = Vector2.Angle(target, rotation);
            if (deltaAngle > MINIMUM_DELTA_ANGLE)
            {
                int sign = 1;
                Vector3 cross = Vector3.Cross((Vector3)target, (Vector3)rotation);
                if (cross.z > 0)
                {
                    sign = -1;
                }
                args.towerTransform.rotation = Quaternion.Euler(0, 0, args.towerTransform.eulerAngles.z + rotatingSpeed * sign);
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
