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
        public override Vector2 TakeAim(AimTakerParameters args)
        {
            var CurrentTarget = args.target;
            Vector2 target = ((Vector2)CurrentTarget.gameObject.transform.position - args.position);
            return target;
        }
    }
}
