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
            Vector3 target = args.direction;
            target.z = 0;
            float zAngle = args.towerTransform.eulerAngles.z + 90;
            zAngle *= Mathf.PI / 180;
            Vector3 rotation = new Vector3(Mathf.Cos(zAngle), Mathf.Sin(zAngle), 0);
            float deltaAngle = Vector3.Angle(target, rotation);
            if (deltaAngle > MINIMUM_DELTA_ANGLE)
            {
                int sign = 1;
                Vector3 cross = Vector3.Cross(target, rotation);
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
