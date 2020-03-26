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
        public abstract void Reload(ReloaderParameters args);
        public abstract void ResetReloading(ReloaderParameters args);
    }
    public class ReloaderParameters
    {
        public bool isLoad;

        public ReloaderParameters(ref bool isLoad)
        {
            this.isLoad = isLoad;
        }
    }
}
