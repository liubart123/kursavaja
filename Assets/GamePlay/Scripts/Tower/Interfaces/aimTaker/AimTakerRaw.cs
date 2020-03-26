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
        public override Vector3 TakeAim(AimTakerParameters args)
        {
            var CurrentTarget = args.target;
            Vector3 target = CurrentTarget.gameObject.transform.position - args.position;
            target.z = 0;
            return target;
        }
    }
}
