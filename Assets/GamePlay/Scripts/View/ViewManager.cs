using Assets.GamePlay.Scripts.Tower.Interfaces.BonusConveyor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.GamePlay.Scripts.View
{
    public class ViewManager : MonoBehaviour
    {
        protected Action CancelAllOperations;
        public void ShowBonusConveyors()
        {
            var conveyors = FindObjectsOfType<BonusConveyor>();
            foreach (var con in conveyors)
            {
                con.ShowConveyor();
            }
            CancelAllOperations += HideBonusConveyors;
        }
        public void HideBonusConveyors()
        {
            var conveyors = FindObjectsOfType<BonusConveyor>();
            foreach (var con in conveyors)
            {
                con.HideConveyor();
            }
            CancelAllOperations -= HideBonusConveyors;
        }
        public void NormalView()
        {
            CancelAllOperations();
        }
    }
}
