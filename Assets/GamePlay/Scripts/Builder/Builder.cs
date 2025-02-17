﻿using Assets;
using Assets.GamePlay.Scripts.Building;
using Assets.GamePlay.Scripts.Player;
using Assets.GamePlay.Scripts.storageTower;
using Assets.GamePlay.Scripts.Tower;
using Assets.scripts.serialization;
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
    public void BuildBuildingOnBlock(Block block, bool buildForOtherPlayers = false)
    {
        BuildBuildingOnBlock(block, arrayOfBuildings[(int)currentBuilding].GetComponent<Building>());
    }
    public void BuildBuildingOnBlock(Block block, EBuilding typeOfBuilding, bool buildForOtherPlayers = false)
    {
        BuildBuildingOnBlock(block, arrayOfBuildings[(int)typeOfBuilding].GetComponent<Building>());
    }
    public Action<Building> OnBuilding;
    PhotonView photonView;
    public void BuildBuildingOnBlock(Block block, Building b, string ownerName = "")
    {
        if (ownerName == "" && enemySpawners != null && !MapSerDeser.isDeserializing) {
            //выклікаецца пы будаўніцтве галоўнага гульца, каб не рабіць лішнія праверкі на кожным кліенце
            if (block.isBasement == false && b.requireBasement == false)
            {
                //калі гэты будынак можа паўплываць на шлях спаўнаў
                if (CheckPathOnThisBlock(block) == false)
                {
                    return;
                }
            }
        }
        if (OnlineManager.BuildForAllPlayers == true && ownerName =="")
        {
            //калі кааператыў і гулец будуе будынак, то пабудаваць будынак у астатніх гульцоў
            photonView.RPC("BuildBuildingOnBlockForOtherPlayers", RpcTarget.Others,
            new Vector3(block.indexes.x, block.indexes.y, 0), b.typeOfBuilding, owner.playerName);
        }
        if (!block.HasBuilding())
        {
            if ((block.isBasement == true && b.requireBasement == true) ||
                (block.isBasement == false && b.requireBasement == false))
            {
                GameObject res;
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
        enemySpawners = null;
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
    //паспрабаваць выдаліць будынак
    public void DestroyBuilding(Building b, bool playingMode = true)
    {
        //выдаляецца безумоўна, калі зараз рэжым рэдактавання ўзроўня
        if (playingMode == false || (b.Owner==owner && b.destroyableInPlayingMode == true))
        {
            owner.buildingsStorage.DestroyBuilding(b.typeOfBuilding);
            b.Die();
            if (PhotonNetwork.IsConnected)
            {
                photonView.RPC("DestroyBuildingForAllPlayers", RpcTarget.Others,
                    new Vector3(b.GetBlock().indexes.x, b.GetBlock().indexes.y, 0));
            }
        }
    }
    [PunRPC]
    public void DestroyBuildingForAllPlayers(Vector3 index)
    {
        Building b = BlocksGenerator.GetBlock(new Vector2Int((int)index.x, (int)index.y))?.GetBuilding()?.GetComponent<Building>();
        if (b != null)
        {
            b.Die();
        }
    }
    public GameObject[] arrayOfBuildings;

    private static EnemySpawner[] enemySpawners;
    public static void FindAllSpawners()
    {
        enemySpawners = FindObjectsOfType<EnemySpawner>();
    }
    private bool CheckPathOnThisBlock(Block block)
    {
        float oldPassability = block.passability;
        block.passability = Mathf.Infinity;
        bool result = true;
        foreach (var spawn in enemySpawners)
        {
            if (spawn.CurrentPath.Contains(block))
            {
                result = spawn.CreatePath();
                if (!result)
                {
                    block.passability = oldPassability;
                    spawn.CreatePath();
                    break;
                }
            }
        }
        block.passability = oldPassability;
        return result;
    }

}
