using Assets.GamePlay.Scripts.Bonuses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using static Assets.GamePlay.Scripts.Bonuses.Bonus;

namespace Assets.GamePlay.Scripts.GUI
{
    public class BuildingsInfoManager : MonoBehaviour
    {
        public GameObject infoTowerPanel;
        public GameObject infoBonusPanel;

        protected BonusesBuilder bonusesBuilder;
        protected Player.MyPlayer owner;
        public void Initialize(Player.MyPlayer pl)
        {
            owner = pl;
            bonusesBuilder = owner.bonusesBuilder;
        }
        public void ShowTowerInfo(Tower.Tower tower)
        {
            ClosePanels();
            infoTowerPanel.SetActive(true);

            infoTowerPanel.transform.GetChild(0).GetChild(0).gameObject.GetComponent<Text>().text = tower.towerName;
            //ачышчаецца форма для інфы
            Transform panelForClasses = infoTowerPanel.transform.GetChild(1);
            for (int i = panelForClasses.childCount-1; i >= 0; i--)
            {
                panelForClasses.GetChild(i).gameObject.SetActive(false);
            }
            Transform panelForConveyor= infoTowerPanel.transform.GetChild(3);
            for (int i = panelForConveyor.childCount - 1; i >= 0; i--)
            {
                panelForConveyor.GetChild(i).gameObject.SetActive(false);
            }
            Transform panelForCombinations = infoTowerPanel.transform.GetChild(5);
            for (int i = panelForCombinations.childCount - 1; i >= 0; i--)
            {
                panelForCombinations.GetChild(i).gameObject.SetActive(false);
            }

            //уводзяцца клясы тавэра
            var classes = tower.classCollection.GetAllClasses();
            for (int i = 0; i < classes.Count; i++)
            {
                GameObject towerClass = infoTowerPanel.transform.GetChild(1).GetChild(i).gameObject;
                towerClass.SetActive(true);
                //towerClass.transform.GetChild(0).gameObject.GetComponent<Text>().text = classes.ElementAt(i).TowerClassName;

                towerClass.GetComponent<Image>().color = classes.ElementAt(i).TowerClassColor;
                towerClass.transform.GetChild(0).gameObject.GetComponent<Text>().text = classes.ElementAt(i).TowerClassName;
            }
            //уводзяцца камібнацыі тавэра
            var combinations = tower.classCollection.GetAllCombinations();
            for (int i = 0; i < combinations.Count; i++)
            {
                GameObject towerClass = infoTowerPanel.transform.GetChild(5).GetChild(i).gameObject;
                towerClass.SetActive(true);
                //towerClass.transform.GetChild(0).gameObject.GetComponent<Text>().text = combinations.ElementAt(i).TowerClassName;

                towerClass.GetComponent<Image>().color = combinations.ElementAt(i).TowerClassColor;
                towerClass.transform.GetChild(0).gameObject.GetComponent<Text>().text = combinations.ElementAt(i).TowerClassName;
            }

            if (tower.bonusConveyor.bonuses != null && tower.bonusConveyor.bonuses.Count != 0)
            {
                int i = 0;
                foreach (var b in tower.bonusConveyor.bonuses)
                {
                    panelForConveyor.GetChild(i).gameObject.SetActive(true);
                    panelForConveyor.GetChild(i).gameObject.GetComponent<Image>().sprite =
                        bonusesBuilder.bonusSprites[(int)b.bonusType];
                    i++;
                }
            }
        }

        protected Bonus selectedBonus;
        public void ShowBonusInfo(Bonuses.Bonus bonus)
        {
            ClosePanels();
            infoBonusPanel.SetActive(true);
            selectedBonus = bonus;
        }
        public void ChangeBonusType(int type)
        {
            bonusesBuilder.ChangeTypeOfBonus((EBonusType)(type), selectedBonus);
        }

        public void ClosePanels()
        {
            infoTowerPanel.SetActive(false);
            infoBonusPanel.SetActive(false);
        }
    }
}
