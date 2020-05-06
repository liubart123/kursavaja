using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.GamePlay.Scripts.Map
{
    public class MapController : MonoBehaviour
    {
        public static void CalculateTowerClassesForAll()
        {
            var towers = FindObjectsOfType<Tower.Tower>();
            foreach(var t in towers)
            {
                t.classCollection.ChangeCombinations();
            }
        }
        Player.MyPlayer owner;
        public void Initialize(Player.MyPlayer pl)
        {
            owner = pl;
            if (pl.bonusesBuilder!=null)
                pl.bonusesBuilder.OnBonusChanging += CalculateTowerClassesForAll;
        }
    }
}
