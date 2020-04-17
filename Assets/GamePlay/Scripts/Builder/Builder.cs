using Assets.GamePlay.Scripts.Building;
using Assets.GamePlay.Scripts.Player;
using Assets.GamePlay.Scripts.Tower;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Builder : MonoBehaviour
{
    public Player owner;
    [SerializeField]
    protected EBuilding currentBuilding;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetBuilding(Building b)
    {
        currentBuilding = b.typeOfBuilding;
        owner.inputControl.IsBuilding = true;
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
        }
    }

    public enum EBuilding
    {
        barrier,
        rawTower
    }
    public GameObject[] arrayOfBuildings;
}
