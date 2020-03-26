using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.GamePlay.Scripts.Tower.Interfaces.reloader
{
    class ReloaderRaw : Reloader
    {
        public short Cooldown;
        protected short currentTick = 0;
        public override void Reload(ReloaderParameters args)
        {
            if (currentTick++ > Cooldown)
            {
                args.isLoad = true;
            }
        }

        public override void ResetReloading(ReloaderParameters args)
        {
            args.isLoad = false;
            currentTick = 0;
        }
    }
}
