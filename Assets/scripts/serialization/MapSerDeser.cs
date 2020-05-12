using Assets.GamePlay.Scripts.Bonuses;
using Assets.GamePlay.Scripts.Building;
using Assets.GamePlay.Scripts.Enemies;
using Assets.GamePlay.Scripts.GUI;
using Assets.GamePlay.Scripts.GUI.TowerCombinationPanel;
using Assets.GamePlay.Scripts.Map;
using Assets.GamePlay.Scripts.Player;
using Assets.GamePlay.Scripts.storageTower;
using Assets.GamePlay.Scripts.Tower;
using Assets.GamePlay.Scripts.Waves;
using Photon.Pun;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static Assets.GamePlay.Scripts.Bonuses.Bonus;
using static Block;
using static Builder;

namespace Assets.scripts.serialization
{
    public class MapSerDeser : MonoBehaviour
    {
        protected Builder builder;
        protected BonusesBuilder bonusesBuilder;
        protected TowerCombinationPanel towerCombinationPanel;
        protected MyPlayer owner;
        public void SaveMap(string fileName = "save.txt")
        {
            //var jsonTextFile = Resources.Load<TextAsset>(filePath);

            WaveManager.EndWave();
            string filePath = Path.Combine(Application.persistentDataPath, fileName);
            SerializeMap(filePath);


        }
        public void LoadMap(string fileName = "save.txt")
        {
            WaveManager.EndWave();
            string filePath = Path.Combine(Application.persistentDataPath, fileName);
            DeserializeMap(filePath);
        }
        protected void SerializeMap(string filePath)
        {
            //WholeSave save = SerializeMapFromJson();
            string json = JsonUtility.ToJson(SerializeMapToJson());
            //string json2 = JsonUtility.ToJson(save.buildings);
            //string json3 = JsonUtility.ToJson(buildings[0]);
            File.WriteAllText(filePath, json);
        }
        public string SerializeMapToJson() {
            WholeSave save = new WholeSave();
            save.buildings = new List<BuildingToSave>();

            save.cellsForTowerCombinationPanel = towerCombinationPanel.GetArrayForSerialization();
            var storage = owner.buildingsStorage;
            save.storages = new List<StorageSerialization>();
            save.storages.Add(new StorageSerialization());
            if (storage != null)
            {
                save.storages.ElementAt(0).money = storage.Money;
                save.storages.ElementAt(0).buildingsInStorage = storage.GetBuildingsForSerialization();
            }
            else
            {
                //дэфолтныя значэнні, калі захоўваецца мапа з лэвэл рэдактара
                save.storages.ElementAt(0).money = 500;
                save.storages.ElementAt(0).costInc = 10;
            }

            var buildings = FindObjectsOfType<Building>();
            foreach (var b in buildings)
            {
                var serBuilding = new BuildingToSave
                {
                    typeOfBuilding = b.typeOfBuilding,
                    indexes = b.GetBlock().indexes,
                    bonusType = (b is Bonus) ? (b as Bonus).bonusType : EBonusType.neutral,
                };
                if (b is Tower && (b as Tower).bonusConveyor.bonuses.Count != 0)
                {
                    serBuilding.bonusIndexes = new Vector2Int[(b as Tower).bonusConveyor.bonuses.Count];
                    for (int i = 0; i < (b as Tower).bonusConveyor.bonuses.Count; i++)
                    {
                        serBuilding.bonusIndexes[i] = (b as Tower).bonusConveyor.bonuses.ElementAt(i).GetBlock().indexes;
                    }
                }
                save.buildings.Add(serBuilding);
            }

            save.blocks = new BlocksForSerialization(BlocksGenerator.width, BlocksGenerator.height);
            Block temp;
            for (int i = 0; i < BlocksGenerator.width; i++)
            {
                for (int j = 0; j < BlocksGenerator.height; j++)
                {
                    temp = BlocksGenerator.blockArray[i, j].GetComponent<Block>();
                    //save.blocks.index[(i * BlocksGenerator.height + j)*2] = temp.indexes.x;
                    //save.blocks.index[(i * BlocksGenerator.height + j) * 2 + 1] = temp.indexes.y;
                    save.blocks.typeOfBlock[i * BlocksGenerator.height + j] = (int)temp.typeOfBlock;
                }
            }

            var waveManager = FindObjectOfType<WaveManager>();
            if (waveManager != null)
            {
                save.waveManager = new WaveManagerSer(waveManager);
            }

            return JsonUtility.ToJson(save);
        }

        public void SaveMapLevel(string fileNameLevel = "saveLevel.txt")
        {

            string filePath = Path.Combine(Application.persistentDataPath, fileNameLevel);
            SerializeMap(filePath);
        }
        public void LoadMapLevel(string fileNameLevel = "saveLevel.txt")
        {
            string filePath = Path.Combine(Application.persistentDataPath, fileNameLevel);
            DeserializeMap(filePath);
        }
        [Serializable]
        public class BuildingToSave
        {
            public Vector2Int indexes;
            public Builder.EBuilding typeOfBuilding;
            public EBonusType bonusType;
            public Vector2Int[] bonusIndexes;
        }
        [Serializable]
        public class WholeSave
        {
            public List<BuildingToSave> buildings;
            [SerializeReference]
            public CellSerializable[] cellsForTowerCombinationPanel;
            [SerializeReference]
            public BlocksForSerialization blocks;
            [SerializeReference]
            public List<StorageSerialization> storages;
            [SerializeReference]
            public WaveManagerSer waveManager;
        }
        [Serializable]
        public class BuildingInStorageSer
        {
            public EBuilding building;
            public int cost;
            public int costInc;
            public BuildingInStorageSer(BuildingInStorage b)
            {
                building = b.building;
                cost = b.cost;
                costInc = b.costIncrease;
            }
            public BuildingInStorageSer()
            {
            }
        }
        [Serializable]
        public class BlocksForSerialization
        {
            public int[] typeOfBlock;
            public BlocksForSerialization()
            {
            }
            public BlocksForSerialization(int width,int height)
            {
                typeOfBlock = new int[width* height];
            }
        }
        [Serializable]
        public class StorageSerialization
        {
            [SerializeReference]
            public List<BuildingInStorageSer> buildingsInStorage;
            public int money;
            public int costInc;
        }
        [Serializable]
        public class WaveManagerSer
        {
            //parameters for waves
            public float maxStartCountOfEnemies;
            public float enemiesCountIncrease;
            public float delayForSpawn;
            public float minSpawnCooldown;
            public float maxSpawnCooldown;
            public int waveCounter;
            public WaveManagerSer()
            {

            }
            public WaveManagerSer(WaveManager waveManager)
            {
                maxStartCountOfEnemies = waveManager.maxStartCountOfEnemies;
                enemiesCountIncrease = waveManager.enemiesCountIncrease;
                delayForSpawn = waveManager.delayForSpawn;
                minSpawnCooldown = waveManager.minSpawnCooldown;
                maxSpawnCooldown = waveManager.maxSpawnCooldown;
                waveCounter = waveManager.waveCounter;
            }
            public void ChangeRealManager(WaveManager waveManager)
            {
                waveManager.maxStartCountOfEnemies = maxStartCountOfEnemies;
                waveManager.enemiesCountIncrease = enemiesCountIncrease;
                waveManager.delayForSpawn = delayForSpawn;
                waveManager.minSpawnCooldown = minSpawnCooldown;
                waveManager.maxSpawnCooldown = maxSpawnCooldown;
                waveManager.waveCounter = waveCounter;

            }
        }
        public static WaveManagerSer DeserializeWaveManager(string json)
        {
            WholeSave save = JsonUtility.FromJson(json, typeof(WholeSave)) as WholeSave;
            return save.waveManager;
        }

        protected void DeserializeMap(string filePath)
        {
            ICollection<Enemy> enemies = FindObjectsOfType<Enemy>();
            foreach (var e in enemies)
            {
                e.Die();
            }
            ICollection<Building> buildings = FindObjectsOfType<Building>();
            foreach (var b in buildings)
            {
                b.Die();
            }

            string json = File.ReadAllText(filePath);
            DeserializeMapFromJson(json);
        }
        public void DeserializeMapFromJson(string json)
        {
            if (json == null || json == "")
            {
                return;
            }
            ICollection<Enemy> enemies = FindObjectsOfType<Enemy>();
            foreach (var e in enemies)
            {
                e.Die();
            }
            ICollection<Building> buildings = FindObjectsOfType<Building>();
            foreach (var b in buildings)
            {
                b.Die();
            }

            //string json = File.ReadAllText(filePath);
            WholeSave save = JsonUtility.FromJson(json, typeof(WholeSave)) as WholeSave;

            for (int i = 0; i < BlocksGenerator.width; i++)
            {
                for (int j = 0; j < BlocksGenerator.height; j++)
                {
                    owner.blocksGenerator.CreateBlock(
                        save.blocks.typeOfBlock[i* BlocksGenerator.height + j],
                        new Vector2Int(i,j));
                    //new Vector2Int(
                    //    save.blocks.index[(i * BlocksGenerator.height + j) * 2],
                    //    save.blocks.index[(i * BlocksGenerator.height + j) * 2 + 1]));
                }
            }


            foreach (var b in save.buildings)
            {
                Block block = BlocksGenerator.blockArray[b.indexes.x, b.indexes.y].GetComponent<Block>();
                builder.ReBuildBuildingOnBlock(block, b.typeOfBuilding);
                if (b.bonusType != EBonusType.neutral)
                {
                    bonusesBuilder.ChangeTypeOfBonus(b.bonusType,
                        block.GetBuilding().GetComponent<Bonus>(), false);
                }
            }


            //creating conveyors
            foreach (var b in save.buildings)
            {
                if (b.bonusIndexes != null)
                {
                    Block block = BlocksGenerator.blockArray[b.indexes.x, b.indexes.y].GetComponent<Block>();
                    Tower tower = block.GetBuilding().GetComponent<Tower>();
                    foreach (var ind in b.bonusIndexes)
                    {
                        var bonus = BlocksGenerator.blockArray[ind.x, ind.y]
                            .GetComponent<Block>().GetBuilding()
                            .GetComponent<Bonus>();
                        tower.bonusConveyor.AddBonus(bonus);
                    }
                }
            }

            towerCombinationPanel.DeserializeCells(save.cellsForTowerCombinationPanel);
            towerCombinationPanel.RefreshAllCells();

            //building storage
            var storage = owner.buildingsStorage;
            if (storage != null && save.storages != null && save.storages.ElementAt(0) != null)
            {
                storage.DeserializeBuildings(save.storages.ElementAt(0).buildingsInStorage);
                storage.Money = save.storages.ElementAt(0).money;
            }


            var waveManager = FindObjectOfType<WaveManager>();
            if (waveManager != null && save.waveManager != null)
            {
                save.waveManager.ChangeRealManager(waveManager);
            }

            MapController.CalculateTowerClassesForAll();
        }

        public string GetJsonOfSavedMap(string fileName)
        {
            string filePath = Path.Combine(Application.persistentDataPath, fileName);
            return File.ReadAllText(filePath);
        }

        private void Start()
        {
            //builder = FindObjectOfType<Builder>();
            //bonusesBuilder = FindObjectOfType<BonusesBuilder>();
            //towerCombinationPanel = FindObjectOfType < TowerCombinationPanel > ();
        }
        public void Initialize()
        {
            //builder = owner.builder;
            //bonusesBuilder = owner.bonusesBuilder;
            //towerCombinationPanel = owner.combinationPanel;
        }
        public void Initialize(MyPlayer pl)
        {
            owner = pl;
            builder = pl.builder;
            bonusesBuilder = pl.bonusesBuilder;
            towerCombinationPanel = pl.combinationPanel;
            Initialize();
        }

    }
}
