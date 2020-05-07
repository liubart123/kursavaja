using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.GamePlay.Scripts.Building.Barriers
{
    public class Barrier : Building
    {
        public override void Initialize()
        {
            base.Initialize();
            GetBlock().passability = Mathf.Infinity;
        }
        public override void Die()
        {
            base.Die();
            GetBlock().ResetPassability();
        }
    }
}
