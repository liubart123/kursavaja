using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static Assets.GamePlay.Scripts.storageTower.BuildingsStorage;
using static Builder;

namespace Assets.GamePlay.Scripts.storageTower
{
    public class BuildingInStorage : MonoBehaviour
    {
        public EBuilding building;
        public int cost;
        public int costIncrease;    //павялічэнне кошту пры кожным будаўніцтве
    }
}
