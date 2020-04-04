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
            if (args.target != null)
            {
                args.bullet.DoShot(args.target);
            } else if (args.direction != null)
            {
                args.bullet.DoShot(args.direction);
            }
        }
    }
}
