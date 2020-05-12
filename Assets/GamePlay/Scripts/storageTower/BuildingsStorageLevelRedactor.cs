using Assets.scripts.serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.GamePlay.Scripts.storageTower
{
    public class BuildingsStorageLevelRedactor : BuildingsStorage
    {
        public void MoneyChange(InputField money)
        {
            try
            {

                Money = Int32.Parse(money.text);
            } catch (Exception e)
            {
                Money = 0;
            }
        }
        public override void Initialize()
        {
            buildings = GetBuildingsInStorage();
            UpdateStoragePanel();
        }
        public override void DestroyBuilding(Builder.EBuilding b)
        {
            //base.DestroyBuilding(b);
        }
        public override void OnBuildingSelection(BuildingInStorage b)
        {
            builder.SetBuilding(builder.arrayOfBuildings.FirstOrDefault(
                el => el.GetComponent<Building.Building>().typeOfBuilding == b.building).
                GetComponent<Building.Building>());
        }
        public override void UpdateStoragePanel()
        {
            for (int i = 0; i < panel.transform.childCount; i++)
            {
                GameObject temp = panel.transform.GetChild(i).gameObject;
                var child = temp.transform.GetChild(0);
                var comp = child.GetComponent<InputField>();
                if (comp == null)
                    continue;   //гэты элемент не будынак
                temp.transform.GetChild(0).gameObject.GetComponent<InputField>().text =
                    temp.GetComponent<BuildingInStorage>().cost.ToString();
                temp.transform.GetChild(1).gameObject.GetComponent<InputField>().text = 
                    temp.GetComponent<BuildingInStorage>().costIncrease.ToString();
            }
        }
        private void UpdateValuesAccordingToInput()
        {
            for (int i = 0; i < panel.transform.childCount; i++)
            {
                GameObject temp = panel.transform.GetChild(i).gameObject;
                if (temp.transform.GetChild(0).gameObject.GetComponent<InputField>() == null)
                    continue;
                Int32.TryParse(temp.transform.GetChild(0).gameObject.GetComponent<InputField>().text,
                    out temp.GetComponent<BuildingInStorage>().cost);

                Int32.TryParse(temp.transform.GetChild(1).gameObject.GetComponent<InputField>().text,
                    out temp.GetComponent<BuildingInStorage>().costIncrease);
                //temp.GetComponent<BuildingInStorage>().costIncrease =
                //    Int32.Parse(
                //    temp.transform.GetChild(1).gameObject.GetComponent<InputField>().text);
            }
        }
        public override List<MapSerDeser.BuildingInStorageSer> GetBuildingsForSerialization()
        {
            UpdateValuesAccordingToInput();
            return base.GetBuildingsForSerialization();
        }
        public override void DeserializeBuildings(List<MapSerDeser.BuildingInStorageSer> des)
        {
            UpdateStoragePanel();
            base.DeserializeBuildings(des);
        }
        public override ICollection<BuildingInStorage> GetBuildingsInStorage()
        {
            List<BuildingInStorage> res = new List<BuildingInStorage>();
            for (int i = 0; i < panel.transform.childCount; i++)
            {
                var temp = panel.transform.GetChild(i).gameObject.GetComponent<BuildingInStorage>();
                if (temp == null)
                    continue;
                res.Add(temp);
            }
            return res;
        }
    }
}
