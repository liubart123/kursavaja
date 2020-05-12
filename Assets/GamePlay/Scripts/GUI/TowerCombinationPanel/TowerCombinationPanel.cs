using Assets.GamePlay.Scripts.GUI.TowerCombinationPanel;
using Assets.GamePlay.Scripts.Map;
using Assets.GamePlay.Scripts.TowerClasses;
using Assets.GamePlay.Scripts.TowerClasses.TowerCombinations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static Assets.GamePlay.Scripts.BulletEffects.BulletEffect;
using static Assets.GamePlay.Scripts.Damage.DamageManager;
using static Assets.GamePlay.Scripts.TowerClasses.TowerClasseGenerator;
using static Assets.GamePlay.Scripts.TowerClasses.TowerCombinations.TowerCombination;

namespace Assets.GamePlay.Scripts.GUI.TowerCombinationPanel
{
    public class TowerCombinationPanel : MonoBehaviour
    {
        protected CombinationGenerator combinationGenerator;
        protected TowerClasseGenerator classGenerator;
        public Sprite[] spritesForEffects;
        public Color[] colorsForEffects;
        public GameObject table;    //аб'кт табліцы ячэек
        public GameObject cell;    //аб'кт ячэйкі
        public Text textForCountOfFreeCells;    //поле, куды запісваць колькасць свабодных ячэяк
        protected Cell selectedCell;
        private int countOfFreeCells;
        protected int CountOfFreeCells
        {
            get { return countOfFreeCells; }
            set {
                countOfFreeCells = value;
                if (textForCountOfFreeCells!=null)
                    textForCountOfFreeCells.text = value.ToString();
            }
        }
        public Dropdown dropDownForCombinations;
        protected Player.MyPlayer owner;
        private void Start()
        {
        }
        private void Awake()
        {
            gameObject.SetActive(true);
        }
        public void Initialize()
        {
            //var player = FindObjectOfType<Player.Player>();
            //combinationGenerator = player.possibleCombinations;
            //classGenerator = player.towerClassCollection;
            cells = new Cell[tableSize, tableSize];
            CreateTable();
            if (SceneManager.GetActiveScene().name == "LevelRedactorScene")
            {
                InitializeDropDown();
                InitializeDropDownDamageType();
                InitializeDropDownTypeEffect();
            }
            CountOfFreeCells = 25;
            gameObject.SetActive(false);
        }
        public void Initialize(Player.MyPlayer pl)
        {
            owner = pl;
            classGenerator = owner.towerClassCollection;
            combinationGenerator = owner.possibleCombinations;
            Initialize();
        }
        protected void CreateTable()
        {
            for (int i = 0; i < tableSize; i++)
            {
                for (int j = 0; j < tableSize; j++)
                {
                    //var temp = Instantiate(cell, transform.GetChild(2)).GetComponent<Cell>();
                    var temp = table.transform.GetChild(i*tableSize+j).gameObject.GetComponent<Cell>();
                    temp.indexes = new Vector2Int(i, j);
                    cells[i, j] = temp;
                    //if (i == 0 && j == 0)
                    //{
                    //    temp.idOfCombination = 0;
                    //}else if (i == 0 && j == tableSize-1)
                    //{
                    //    temp.idOfCombination = 1;
                    //}
                    //else if (i == tableSize-1 && j == 0)
                    //{
                    //    temp.idOfCombination = 2;
                    //}
                    //else if (i == tableSize-1 && j == tableSize-1)
                    //{
                    //    temp.idOfCombination = 3;
                    //}
                    RefreshCell(temp);
                    //table.transform.GetChild(i * tableSize + j).gameObject.GetComponent<Cell>().indexes = temp.indexes;

                }
            }
        }
        
        protected int tableSize = 5;
        public Cell[,] cells;

        public Dropdown dropdownTypeOfCell;
        protected string dropdownNameForEffectChosed = "effect";
        protected void InitializeDropDown()
        {
            List<Dropdown.OptionData> options = new List<Dropdown.OptionData>();

            foreach (var cl in classGenerator.towerClasess)
            {
                var temp = new Dropdown.OptionData(cl.TowerClassName);
                options.Add(temp);
            }
            options.Add(new Dropdown.OptionData(dropdownNameForEffectChosed));

            dropdownTypeOfCell.options = options;
        }

        public Dropdown dropdownTypeOfDamage;
        protected void InitializeDropDownDamageType()
        {
            List<Dropdown.OptionData> options = new List<Dropdown.OptionData>();

            foreach (string name in Enum.GetNames(typeof(EKindOfDamage)))
            {
                options.Add(new Dropdown.OptionData(name));
            }

            dropdownTypeOfDamage.options = options;
        }
        public Dropdown dropdownTypeOfEffect;
        protected void InitializeDropDownTypeEffect()
        {
            List<Dropdown.OptionData> options = new List<Dropdown.OptionData>();

            options.Add(new Dropdown.OptionData("immidiate damage"));
            options.Add(new Dropdown.OptionData("periodic damage"));
            options.Add(new Dropdown.OptionData("slowing"));

            dropdownTypeOfEffect.options = options;
        }

        private void Update()
        {
            //if (Input.mouseScrollDelta.y > 0)
            //{
            //    width += scroll;
            //    height += scroll;
            //    GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
            //}else if ((Input.mouseScrollDelta.y < 0))
            //{
            //    width -= scroll;
            //    height -= scroll;
            //    GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
            //} 
        }

        //дабавіць да ячэйкі спасылку на кляс ці эфект
        public void CreateCell()
        {
            CreateDataFormForm();
            if (currentData.isItClass)
            {
                selectedCell.towerClass = currentData.towerClass;
                selectedCell.typeOfClass = currentData.towerClass.typeOfTower;
                selectedCell.bulletEffect = null;
            }
            else
            {
                selectedCell.bulletEffect = currentData.effect;
                selectedCell.towerClass = null;
                selectedCell.colorOfEffect = currentData.colorOfEffect;

                //selectedCell.bulletEffectType = currentData.effect.typeOfEffect;
                //selectedCell.valueOfEffect = currentData.effect.Effectivity;

            }
            RefreshCell(selectedCell);
            RefreshCellInfo(selectedCell);
        }
        //выбраць цякучую ячэйку
        public void SelectCell(Cell cell) {
            //cell.indexes = new Vector2Int(123, 321);
            selectedCell = cell;
            RefreshCellInfo(cell);
        }
        //аднавіць змесціва ячэйкі згодна з дадзенымі з формы, тыпа колер і тэкст
        public void RefreshCell(Cell cell)
        {
            if (cell.towerClass != null)
            {
                cell.transform.GetChild(0).gameObject.GetComponent<Text>().color = cell.towerClass.TowerClassColor;
                cell.transform.GetChild(0).gameObject.SetActive(true);
                cell.transform.GetChild(2).gameObject.SetActive(false);
                cell.transform.GetChild(0).gameObject.GetComponent<Text>().text =
                    cell.towerClass.TowerClassName;
            } else if (cell.bulletEffect!=null)
            {
                cell.transform.GetChild(0).gameObject.GetComponent<Text>().color = Color.white;
                cell.transform.GetChild(0).gameObject.SetActive(false);
                cell.transform.GetChild(2).gameObject.SetActive(true);
                cell.transform.GetChild(2).gameObject.GetComponent<Image>().sprite =
                    spritesForEffects[(int)cell.bulletEffect.typeOfEffect];
                cell.transform.GetChild(2).gameObject.GetComponent<Image>().color = cell.colorOfEffect;
            }
            else
            {
                cell.transform.GetChild(0).gameObject.GetComponent<Text>().color = Color.white;
                cell.transform.GetChild(0).gameObject.GetComponent<Text>().text = "";
                cell.transform.GetChild(0).gameObject.SetActive(false);
                cell.transform.GetChild(2).gameObject.SetActive(false);
            }
            if (cell.idOfCombination == -1)
            {
                cell.transform.GetChild(1).GetComponent<Image>().color = Color.gray;
            } else
            {
                cell.transform.GetChild(1).GetComponent<Image>().color =
                    combinationGenerator.GetTowerCombination((ETypeOfCombination)cell.idOfCombination).TowerClassColor;
            }
        }
        public void RefreshAllCells()
        {
            for (int i = 0; i < tableSize; i++)
            {
                for (int j = 0; j < tableSize; j++)
                {
                    RefreshCell(cells[i, j]);
                }
            }
        }
        public InputField valueOfEffect;
        //атрымаць дадзеныя з формы
        public void CreateDataFormForm()
        {
            if (dropdownTypeOfCell.value >= classGenerator.towerClasess.Count)
            {
                currentData.isItClass = false;
                try
                {
                    switch (dropdownTypeOfEffect.value)
                    {
                        case 0:
                            currentData.effect =
                                new BulletEffects.BulletEffectImmidiateDamageRaw(
                                    float.Parse(valueOfEffect.text),
                                    (EKindOfDamage)dropdownTypeOfDamage.value);
                            currentData.colorOfEffect = colorsForEffects[dropdownTypeOfDamage.value];
                            break;
                        case 1:
                            currentData.effect =
                                new BulletEffects.BulletEffectPeriodicDamageRaw(
                                    float.Parse(valueOfEffect.text),
                                    (EKindOfDamage)dropdownTypeOfDamage.value);
                            currentData.colorOfEffect = colorsForEffects[dropdownTypeOfDamage.value];
                            break;
                        case 2:
                            currentData.effect =
                                new BulletEffects.BulletEffectSlowingRaw(
                                    float.Parse(valueOfEffect.text));
                            break;
                    }
                } catch (Exception e)
                {
                    currentData.effect = null;
                }
            }
            else
            {
                currentData.isItClass = true;
                currentData.towerClass = classGenerator.GetTowerClass((ETowerClass)dropdownTypeOfCell.value);
            }
        }
        DataFromForm currentData = new DataFromForm();
        public class DataFromForm
        {
            //name of class or effect
            public bool isItClass = false;
            public TowerClass towerClass;
            public BulletEffects.BulletEffect effect;
            public EKindOfDamage typeOfDamage;
            public float effectValue;
            public Color colorOfEffect;
        }

        public Text cellInfo;
        //аднавіць тэкст з апісаннем ячэйкі
        public void RefreshCellInfo(Cell cell)
        {
            cellInfo.text = cell.ToString();
        }

        public CellSerializable[] GetArrayForSerialization()
        {
            CellSerializable[] cellsSer = new CellSerializable[tableSize*tableSize];
            for (int i = 0; i < tableSize; i++)
            {
                for (int j = 0; j < tableSize; j++)
                {
                    cellsSer[i*tableSize +  j] = new CellSerializable(cells[i, j]);
                }
            }
            return cellsSer;
        }
        public void DeserializeCells(CellSerializable[] cellsS)
        {
            for (int i = 0; i < tableSize; i++)
            {
                for (int j = 0; j < tableSize; j++)
                {
                    cells[i, j].typeOfClass = cellsS[i * tableSize + j].typeOfClass;
                    cells[i, j].bulletEffect = cellsS[i * tableSize + j].bulletEffect;
                    cells[i, j].indexes = cellsS[i * tableSize + j].indexes;
                    cells[i, j].idOfCombination = cellsS[i * tableSize + j].idOfCombination;
                    cells[i, j].colorOfEffect = cellsS[i * tableSize + j].colorOfEffect;
                    if (cellsS[i * tableSize + j].isThereClass)
                    {
                        cells[i, j].towerClass = classGenerator.GetTowerClass(cells[i, j].typeOfClass);
                    }
                    if (cellsS[i * tableSize + j].idOfCombination != -1)
                    {
                        ChangeTowerCombinations(cells[i, j]);
                    }
                }
            }
        }
        //колькі суседніх ячэяк пралічваць, каб трэба можна было дадць да ячэйкі камбінацыю
        int countOfNeighbours = 4;
        public void ChangeCombinationOfCell()
        {
            if (dropDownForCombinations.value == 4)
            {
                if (selectedCell.idOfCombination!=-1)
                    CountOfFreeCells++;
                selectedCell.idOfCombination = -1;
            }
            else
            {
                //нельга змяніц камбінацыю ячэйкам у кутах
                if (selectedCell.indexes==Vector2Int.zero
                    || selectedCell.indexes==new Vector2Int(0,tableSize-1)
                    || selectedCell.indexes == new Vector2Int(tableSize - 1, tableSize - 1)
                    || selectedCell.indexes == new Vector2Int(tableSize - 1, 0))
                {
                    return;
                }
                //калі няма свабодных ячэек 
                if (CountOfFreeCells <= 0)
                {
                    return;
                }
                Vector2Int neighbourPos;
                bool isThereNeighbour = false;  //ці ёсць сусед з такой жа камбінацыяй
                for (int i = 0; i < countOfNeighbours; i++)
                {
                    neighbourPos = new Vector2Int(
                        (int)Mathf.Cos((float)i / countOfNeighbours * Mathf.PI * 2),
                        (int)Mathf.Sin((float)i / countOfNeighbours * Mathf.PI * 2));
                    neighbourPos += selectedCell.indexes;
                    if (neighbourPos.x<0|| neighbourPos.x>=tableSize ||
                        neighbourPos.y<0 || neighbourPos.y >= tableSize)
                    {
                        continue;
                    }
                    if (cells[neighbourPos.x, neighbourPos.y].idOfCombination
                        == dropDownForCombinations.value)
                    {
                        isThereNeighbour = true;
                        break;
                    }
                }
                if (isThereNeighbour)
                {
                    selectedCell.idOfCombination = dropDownForCombinations.value;
                    ChangeTowerCombinations(selectedCell);
                    CountOfFreeCells--;
                }
            }
            RefreshCell(selectedCell);
            owner.players?.UpdatePlayerForOtherPlayers();
            MapController.CalculateTowerClassesForAll();
        }
        //змяніць камбінацыі тавэроў, згодна з ячэйкай
        private void ChangeTowerCombinations(Cell cell)
        {
            TowerCombination comb = combinationGenerator.GetTowerCombination((ETypeOfCombination)cell.idOfCombination);
            if (cell.towerClass != null)
            {
                if (!comb.towerClasses.Contains(cell.towerClass.typeOfTower))
                {
                    comb.towerClasses.Add(cell.towerClass.typeOfTower);
                }
            }
            else if (cell.bulletEffect != null)
            {
                if (!comb.bulletEffects.Contains(cell.bulletEffect))
                {
                    comb.bulletEffects.Add(cell.bulletEffect);
                }
            }
        }
    }
}
