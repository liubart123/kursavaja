using Assets.GamePlay.Scripts.TowerClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using static Assets.GamePlay.Scripts.TowerClasses.TowerClasseGenerator;
using static Assets.scripts.serialization.MapSerDeser;
using static Builder;

namespace Assets.GamePlay.Scripts.storageTower
{
    public class BuildingsStorage : MonoBehaviour
    {
        protected int money;
        public int Money { get 
            { return money; } 
            set {
                money = value;
                if (textForMoneyValue!=null)
                    textForMoneyValue.text = value.ToString();
            } }
        public float partOfMoneyToReturn;
        public ICollection<BuildingInStorage> buildings;
        public TowerChoosingPanel choosingPanel;
        public GameObject panel;
        public GameObject towerButton;
        public Text textForMoneyValue;
        protected Builder builder;

        private void Awake()
        {
            var temp = Money;
            Money = temp;
        }

        protected Player.MyPlayer owner;

        private void Update()
        {
            //if (Input.GetKeyDown(KeyCode.Space))
            //{
            //    UpdateStoragePanel();
            //}
        }
        public virtual void Initialize()
        {
            buildings = GetBuildingsInStorage();
            UpdateStoragePanel();
            builder.OnBuilding = (b) =>
            {
                BuildBuilding(b.typeOfBuilding);
            };
            Money = 500;
        }

        public void Initialize(Player.MyPlayer pl)
        {
            owner = pl;
            builder = pl.builder;
            Initialize();
        }
        //пры будаўніцтве будынка
        public void BuildBuilding(EBuilding b)
        {
            foreach (var building in buildings)
            {
                if (building.building == b)
                {
                    Money -= building.cost;
                    UpdateStoragePanel();
                    if (Money < building.cost)
                    {
                        if (owner != null)
                        {
                            owner.inputControl.TypeOfAction= InputControl.ETypeOfInputAction.nothing;
                        }
                    }
                    building.cost += building.costIncrease;
                    OnBuildingSelection(null);
                    UpdateStoragePanel();
                    break;
                }
            }
        }
        public virtual void DestroyBuilding(EBuilding b)
        {
            foreach (var building in buildings)
            {
                if (building.building == b)
                {
                    Money += (int)(building.cost * partOfMoneyToReturn);
                    UpdateStoragePanel();
                    break;
                }
            }
        }
        //аднавіць выгляд панэлі 
        public virtual void UpdateStoragePanel()
        {
            for (int i = 0; i < panel.transform.childCount; i++)
            {
                GameObject temp = panel.transform.GetChild(i).gameObject;

                temp.transform.GetChild(0).gameObject.GetComponent<Text>().text
                    = temp.GetComponent<BuildingInStorage>().cost.ToString();
            }
        }

        private BuildingInStorage prevBuilding;
        //выбар будынка для будаўніцтва
        public virtual void OnBuildingSelection(BuildingInStorage b)
        {
            if (b == null)
            {
                if (prevBuilding != null)
                {
                    b = prevBuilding;
                }
                else
                    return;
            }
            if (b.cost > Money)
            {
                return;
            }
            prevBuilding = b;
            builder.SetBuilding(builder.arrayOfBuildings.FirstOrDefault(
                el => el.GetComponent<Building.Building>().typeOfBuilding == b.building).
                GetComponent<Building.Building>());
        }
        public virtual ICollection<BuildingInStorage> GetBuildingsInStorage()
        {
            List<BuildingInStorage> res = new List<BuildingInStorage>();
            for (int i = 0; i < panel.transform.childCount; i++)
            {
                res.Add(panel.transform.GetChild(i).gameObject.GetComponent<BuildingInStorage>());
            }
            return res;
        }

        public virtual List<BuildingInStorageSer> GetBuildingsForSerialization()
        {
            List<BuildingInStorageSer> res = new List<BuildingInStorageSer>();
            foreach (var b in buildings)
            {
                res.Add(new BuildingInStorageSer(b));
            }
            return res;
        }
        public virtual void DeserializeBuildings(List<BuildingInStorageSer> des)
        {
            foreach (var b in des)
            {
                var building = buildings.FirstOrDefault(el => el.building == b.building);
                if (building != null)
                {
                    building.cost = b.cost;
                    building.costIncrease = b.costInc;
                }
            }
            UpdateStoragePanel();
        }
        
    }
}
