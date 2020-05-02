using Assets;
using Assets.GamePlay.Scripts.Building;
using Assets.GamePlay.Scripts.Player;
using Assets.GamePlay.Scripts.storageTower;
using Assets.GamePlay.Scripts.Tower;
using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Builder : MonoBehaviour
{
    public Player owner;
    [SerializeField]
    protected EBuilding currentBuilding;

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
    public Action<Building> OnBuilding;
    public void BuildBuildingOnBlock(Block block, Building b)
    {
        if (!block.HasBuilding())
        {
            GameObject res;
            if (OnlineManager.CreateNetworkObjects)
            {
                res = PhotonNetwork.Instantiate(b.gameObject.name, block.transform.position, block.transform.rotation);

            }
            else { 
                res = Instantiate(b.gameObject, block.transform.position, block.transform.rotation);
            }
            res.transform.parent = block.transform;
            res.GetComponent<Building>().owner = owner;
            res.GetComponent<Building>().Initialize();
            OnBuilding?.Invoke(res.GetComponent<Building>());
            //if (towerStorage != null)
            //{
            //    towerStorage.RemoveBuilding(res.GetComponent<Building>().typeOfBuilding);
            //}
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
        //towerStorage = FindObjectOfType<BuildingsStorage>();
    }
    public void Initialize(Player pl)
    {
        owner = pl;
        Initialize();
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
