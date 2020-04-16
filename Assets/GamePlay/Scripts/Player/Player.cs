using Assets.GamePlay.Scripts.TowerClasses;
using Assets.GamePlay.Scripts.TowerClasses.TowerCombinations;
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

        public void Start()
        {
            possibleCombinations = FindObjectOfType<CombinationGenerator>();
            towerClassCollection = FindObjectOfType<TowerClasseGenerator>();
            inputControl = FindObjectOfType<InputControl>();
            inputControl.owner = this;
            builder = FindObjectOfType<Builder>();
            builder.owner = this;
            towerClassCollection.Initialize();
            possibleCombinations.Initialize();
            InitializeTowers();

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
