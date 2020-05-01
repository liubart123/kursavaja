using Assets.GamePlay.Scripts.Building;
using Assets.GamePlay.Scripts.Player;
using Assets.GamePlay.Scripts.storageTower;
using Assets.GamePlay.Scripts.Tower;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Builder : MonoBehaviour
{
    public Player owner;
    [SerializeField]
    protected EBuilding currentBuilding;
    protected BuildingsStorage towerStorage;

    public void SetBuilding(Building b)
    {
        currentBuilding = b.typeOfBuilding;
        owner.inputControl.TypeOfAction = InputControl.ETypeOfInputAction.build;
    }
    public void BuildBuildingOnBlock(Block block)
    {
        BuildBuildingOnBlock(block, arrayOfBuildings[(int)currentBuilding].GetComponent<Building>());
    }
    public void BuildBuildingOnBlock(Block block, EBuilding typeOfBuilding)
    {
        BuildBuildingOnBlock(block, arrayOfBuildings[(int)typeOfBuilding].GetComponent<Building>());
    }

    public void BuildBuildingOnBlock(Block block, Building b)
    {
        if (!block.HasBuilding())
        {
            GameObject res = Instantiate(b.gameObject, block.transform.position, block.transform.rotation);
            res.transform.parent = block.transform;
            res.GetComponent<Building>().owner = owner;
            res.GetComponent<Building>().Initialize();
            if (towerStorage != null)
            {
                towerStorage.WasteMoney(res.GetComponent<Building>().typeOfBuilding);
            }
        }
    }
    public void ReBuildBuildingOnBlock(Block block, EBuilding typeOfBuilding)
    {
        for (int i = block.transform.childCount - 1; i >= 0; i--)
        {
            block.transform.GetChild(i).gameObject.GetComponent<Building>()?.Die();
        }
        block.transform.DetachChildren();
        BuildBuildingOnBlock(block, arrayOfBuildings[(int)typeOfBuilding].GetComponent<Building>());
    }

    public void Initialize()
    {
        towerStorage = FindObjectOfType<BuildingsStorage>();
    }
    public enum EBuilding
    {
        barrier,
        enemySpawner,
        jungleTower,
        vintTower,
        oliveTower,
        indigoTower,
        mangoTower,
        narcissTower,
        enemySpawnerPersian,
        enemySpawnerCherry,
        bonus
    }
    public GameObject[] arrayOfBuildings;
}
