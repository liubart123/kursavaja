using Assets.GamePlay.Scripts.GUI;
using Assets.GamePlay.Scripts.GUI.TowerCombinationPanel;
using Assets.GamePlay.Scripts.storageTower;
using Assets.GamePlay.Scripts.TowerClasses;
using Assets.GamePlay.Scripts.TowerClasses.TowerCombinations;
using Assets.scripts.serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.GamePlay.Scripts.Player
{
    public class Player : MonoBehaviour
    {
        public CombinationGenerator possibleCombinations;
        public TowerClasseGenerator towerClassCollection;
        public InputControl inputControl;
        public Builder builder;
        public BuildingsStorage towerStorage;

        public void Start()
        {
            possibleCombinations = FindObjectOfType<CombinationGenerator>();
            towerClassCollection = FindObjectOfType<TowerClasseGenerator>();

            towerStorage = FindObjectOfType<BuildingsStorage>();
            if (towerStorage != null)
            {
                towerStorage.Initialize();
                towerStorage.owner = this;
            }

            inputControl = FindObjectOfType<InputControl>();
            inputControl.owner = this;

            FindObjectOfType<BlocksGenerator>()?.Initialize();

            builder = FindObjectOfType<Builder>();
            builder.owner = this;
            builder.Initialize();

            towerClassCollection.Initialize();
            possibleCombinations.Initialize();

            FindObjectOfType<TowerCombinationPanel>().Initialize();
            FindObjectOfType<MapSerDeser>().LoadMapLevel();

        }
        private void InitializeTowers()
        {
            var towers = GameObject.FindObjectsOfType<Tower.Tower>();
            foreach (var tower in towers)
            {
                tower.owner = this;
                tower.Initialize();
            }
        }
    }
}
