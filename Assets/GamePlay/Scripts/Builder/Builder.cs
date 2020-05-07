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
    public MyPlayer owner;
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
    PhotonView photonView;
    public void BuildBuildingOnBlock(Block block, Building b, string ownerName = "")
    {
        if (OnlineManager.CreateNetworkObjects)
        {
            photonView.RPC("BuildBuildingOnBlockForOtherPlayers", RpcTarget.Others,
                new Vector3(block.indexes.x, block.indexes.y, 0), b.typeOfBuilding, owner.playerName);
        }
        if (!block.HasBuilding())
        {
            if ((block.isBasement == true && b.requireBasement == true) ||
                (block.isBasement == false && b.requireBasement == false))
            {
                GameObject res;
                //if (OnlineManager.CreateNetworkObjects)
                //{
                //    res = PhotonNetwork.Instantiate(b.gameObject.name, block.transform.position, block.transform.rotation);
                //}
                //else
                //{
                //    res = Instantiate(b.gameObject, block.transform.position, block.transform.rotation);
                //}
                res = Instantiate(b.gameObject, block.transform.position, block.transform.rotation);

                res.transform.parent = block.transform;
                if (ownerName=="")  //будынак створаны гульцэом на гэтым кампутары
                    res.GetComponent<Building>().Owner = owner;
                else
                {
                    //створаны іншымі гульцамі
                    res.GetComponent<Building>().OwnerName = ownerName;
                    res.GetComponent<Building>().Owner = owner.players.GetPlayerByName(ownerName);
                }
                res.GetComponent<Building>().Initialize();
                OnBuilding?.Invoke(res.GetComponent<Building>());
            }
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

    [PunRPC]
    public void BuildBuildingOnBlockForOtherPlayers(Vector3 block, int typeOfBuild, string ownerName)
    {
        Block b = BlocksGenerator.GetBlock(new Vector2Int((int)block.x, (int)block.y));
        Building bd = arrayOfBuildings[typeOfBuild].GetComponent<Building>();
        BuildBuildingOnBlock(b, bd, ownerName);
    }
    public void Initialize()
    {
        photonView = GetComponent<PhotonView>();
        //towerStorage = FindObjectOfType<BuildingsStorage>();
    }
    public void Initialize(MyPlayer pl)
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
