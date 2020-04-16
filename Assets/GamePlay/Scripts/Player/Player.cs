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

        public void Start()
        {
            possibleCombinations = FindObjectOfType<CombinationGenerator>();
            towerClassCollection = FindObjectOfType<TowerClasseGenerator>();
            towerClassCollection.Initialize();
            possibleCombinations.Initialize();
        }
    }
}
