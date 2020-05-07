using Assets.GamePlay.Scripts.Bonuses;
using Assets.GamePlay.Scripts.Waves;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.forTest.script
{
    public class TestingDifferenceBetweenWaves : MonoBehaviour
    {
        public WaveManager waveManager;
        public int countOfCycles;
        public int counter = 0;
        private void Start()
        {
            WaveManager.OnWaveEnd += CountTowerDamage;
            fileName = "difference" + UnityEngine.Random.Range(0, 1000);
        }
        private string fileName;
        private async void CountTowerDamage()
        {
            if (counter++ > countOfCycles)
            {
                WaveManager.OnWaveEnd -= CountTowerDamage;
                return;
            }
            string filePath = Path.Combine(Application.persistentDataPath, fileName);

            ICollection<Bonus> bonuses = FindObjectsOfType<Bonus>();
            List<string> values = new List<string>();
            using (StreamWriter sw = new StreamWriter(filePath, true))
            {
                sw.Write(counter.ToString() + ": ");
                foreach (var b in bonuses)
                {
                    values.Add(b.healthController.Damage.ToString());

                    sw.Write(b.healthController.Damage.ToString() + ", ");

                    b.healthController.Damage = 0;
                }
                sw.WriteLine("\n");
            }
            await Task.Delay(5000);
            waveManager.StartWave();
        }
    }
}
