using Assets.GamePlay.Scripts.Ammo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.GamePlay.Scripts.Tower.Interfaces
{
    public abstract class Reloader : MonoBehaviour
    {
        public abstract bool Reload(ReloaderParameters args);
        public abstract void ResetReloading(ReloaderParameters args);
    }
    public class ReloaderParameters
    {
        public Boolean isLoad;

        public ReloaderParameters(ref Boolean isLoad)
        {
            this.isLoad = isLoad;
        }
    }
}
