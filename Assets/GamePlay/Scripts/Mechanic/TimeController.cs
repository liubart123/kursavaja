using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.GamePlay.Scripts.Mechanic
{
    public class TimeController : MonoBehaviour
    {
        public void ChangeTimeSpeed(float scale)
        {
            Time.timeScale = scale;
        }
    }
}
