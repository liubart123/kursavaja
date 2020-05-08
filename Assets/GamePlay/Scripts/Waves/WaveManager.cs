using Photon.Pun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.GamePlay.Scripts.Waves
{
    public class WaveManager : MonoBehaviour
    {
        protected static ICollection<EnemySpawner> enemySpawners;
        protected static ICollection<Tower.Tower> towers;
        private static bool isThereWave = false;
        public static bool IsThereWave
        {
            get { return isThereWave; }
            set { 
                isThereWave = value;
            }
        }
        public static Action OnWaveStart, OnWaveEnd;

        //parameters for waves
        public float maxStartCountOfEnemies;
        public float enemiesCountIncrease;
        public float delayForSpawn;
        public float minSpawnCooldown;
        public float maxSpawnCooldown;
        public int waveCounter = 0;

        public static void Initialize()
        {
            FindAllEnemySpawners();
            FindAllTowers();
        }
        public static void FindAllEnemySpawners()
        {
            enemySpawners = FindObjectsOfType<EnemySpawner>();
        }
        public static void FindAllTowers()
        {
            towers = FindObjectsOfType<Tower.Tower>();
        }
        public static void EndWave()
        {
            if (IsThereWave)
            {
                IsThereWave = false;
                OnWaveEnd?.Invoke();
            }
        }
        //магчыма заканчэнне хвалі
        public static void EndWavePossible()
        {
            if (enemySpawners.Any(el => el.isSpawning))
                return;
            EndWave();
        }
        [PunRPC]
        public void StartWave(float[] countVector = null, float[] cooldownVector = null, float[] delayVector = null)
        {
            FindAllTowers();
            foreach (var t in towers)
            {
                //t.InitializeBulletFactory();
                t.ResetState();
            }


            FindAllEnemySpawners();
            int i = 0;
            foreach (var sp in enemySpawners)
            {
                sp.enemiMaxCount = (int)(countVector[i] * sp.enemieCountKaef);
                sp.cooldown = (int)(cooldownVector[i] * sp.enemieCooldownKaef);
                sp.delay = (int)(delayVector[i]);
                sp.StartSpawning();
                i++;
            }
            OnWaveStart?.Invoke();
            IsThereWave = true;
            waveCounter++;
        }

        private void CreateParametersForSpawns(out float[] countVector, out float[] cooldownVector, out float[] delayVector)
        {
            FindAllEnemySpawners();
            countVector = GetRandomVector(0, 1, enemySpawners.Count);
            NormalizeVector(ref countVector);
            VectorMult(ref countVector, maxStartCountOfEnemies);

            cooldownVector = GetRandomVector(0, 1, enemySpawners.Count);
            NormalizeVector(ref cooldownVector);
            VectorMult(ref cooldownVector, maxSpawnCooldown - minSpawnCooldown);
            VectorSum(ref cooldownVector, minSpawnCooldown);

            delayVector = GetRandomVector(0, 1, enemySpawners.Count);
            NormalizeVector(ref delayVector);
            VectorMult(ref delayVector, delayForSpawn);





            maxStartCountOfEnemies += enemiesCountIncrease;

        }
        private float[] GetRandomVector(float min, float max, int size)
        {
            float[] res = new float[size];
            for(int i = 0; i < size; i++)
            {
                res[i] = GetRandomFloat(min, max);
            }
            return res;
        }
        private void NormalizeVector(ref float[] vector)
        {
            float len = 0;
            for(int i = 0; i < vector.Length; i++)
            {
                len += Mathf.Pow(vector[i],2);
            }
            len = Mathf.Sqrt(len);
            for (int i = 0; i < vector.Length; i++)
            {
                vector[i] = vector[i] / len;
            }
        }
        private void VectorMult(ref float[] vector, float num)
        {
            for (int i = 0; i < vector.Length; i++)
            {
                vector[i] *= num;
            }
        }
        private void VectorSum(ref float[] vector, float num)
        {
            for (int i = 0; i < vector.Length; i++)
            {
                vector[i] += num;
            }
        }

        public void StartWaveForAllClients()
        {
            rnd = new System.Random(waveCounter+rnd.Next());
            float[] countVector = null, cooldownVector = null, delayVector = null;
            CreateParametersForSpawns(out countVector, out cooldownVector, out delayVector);
            if (PhotonNetwork.IsConnected)
                GetComponent<PhotonView>().RPC("StartWave", RpcTarget.All, countVector, cooldownVector, delayVector);
            else
                StartWave(countVector, cooldownVector, delayVector);
        }

        private System.Random rnd = new System.Random(DateTime.Now.Second);
        private float GetRandomFloat(float min, float max)
        {
            return (float)(rnd.NextDouble()%(max-min)+min);
        }
    }
}
