using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.GamePlay.Scripts.Waves
{
    public class DeactivationOnWaveStarting : MonoBehaviour
    {
            
        private void Start()
        {
            WaveManager.OnWaveStart += Deactivate;
            WaveManager.OnWaveEnd += Activate;
            //Debug.Log(gameObject.name + " setted action on waveStarting");
        }
        private void OnDestroy()
        {

            WaveManager.OnWaveStart -= Deactivate;
            WaveManager.OnWaveEnd -= Activate;
            //Debug.Log(gameObject.name + " removed action on waveStarting");
        }
        public void Activate()
        {
            gameObject.SetActive(true);
        }
        public void Deactivate()
        {
            gameObject.SetActive(false);
        }
    }
}
