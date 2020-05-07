using Photon.Pun.Demo.PunBasics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.GamePlay.Scripts.Player
{
    public class PlayerOnline : MyPlayer
    {
        public override void Start()
        {
            base.Start();
            FindObjectOfType<Launcher>().Connect();
        }
    }
}
