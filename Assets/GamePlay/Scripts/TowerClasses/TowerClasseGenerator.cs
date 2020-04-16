using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.GamePlay.Scripts.TowerClasses
{
    //кляс, для генерацыі клясаў веж адпаведна параметрам. У кожнага гульца свой экзэмлпяр
    public class TowerClasseGenerator : MonoBehaviour
    {
        public ICollection<TowerClass> towerClasess;
        public TowerClass GetTowerClass(Type t)
        {
            return towerClasess.First(el => t == el.GetType());
        }

        private void Start()
        {
        }
        public void Initialize()
        {
            towerClasess = new List<TowerClass>();
            towerClasess.Add(new TowerClassRaw1());
            towerClasess.Add(new TowerClassRaw2());
            towerClasess.Add(new TowerClassRaw3());
            towerClasess.Add(new TowerClassRaw4());
        }
    }
}
