using Assets.GamePlay.Scripts.Bonuses;
using Assets.GamePlay.Scripts.GUI;
using Assets.GamePlay.Scripts.GUI.TowerCombinationPanel;
using Assets.GamePlay.Scripts.Map;
using Assets.GamePlay.Scripts.Mechanic;
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
        public BonusesBuilder bonusesBuilder;
        public GuiControl guiControl;
        public TowerCombinationPanel combinationPanel;
        public TimeController timeController;
        public BuildingsStorage buildingsStorage;
        public MapSerDeser mapSerDeser;
        public MapController mapController;

        public void Start()
        {
            towerClassCollection.Initialize(this);
            possibleCombinations.Initialize(this);
            inputControl.Initialize(this);
            builder.Initialize(this);
            bonusesBuilder?.Initialize(this);
            guiControl.Initialize(this);
            combinationPanel.Initialize(this);
            //timeController.Initialize();
            buildingsStorage?.Initialize(this);
            mapSerDeser.Initialize(this);
            mapController?.Initialize(this);

            FindObjectOfType<BlocksGenerator>()?.Initialize();


            mapSerDeser.LoadMapLevel();
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
