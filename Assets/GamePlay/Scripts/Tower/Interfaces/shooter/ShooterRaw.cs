using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.GamePlay.Scripts.Tower.Interfaces.shooter
{
    class ShooterRaw : Shooter
    {
        public override void Shoot(ShooterParameters args)
        {
            args.bullet.DoShot(args.target);
        }
    }
}
