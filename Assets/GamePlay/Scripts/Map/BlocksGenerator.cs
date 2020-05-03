using Assets;
using Assets.GamePlay.Scripts.Player;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Block;

public class BlocksGenerator : MonoBehaviour
{
    public static int width = 60;
    public static int height = 40;
    public GameObject blockObject;
    public GameObject[] typesOfBlocks;
    public static GameObject[,] blockArray;
    public Vector2 startPos;
    public Vector2 BlockSize;
    public ETypeOfBlock typeOfBlockToBuild;
    private Player owner;
    // Start is called before the first frame update
    void Start()
    {
        //GenerateBlocks();
    }
    public void Initialize(Player pl)
    {
        owner = pl;
        if (OnlineManager.CreateNetworkObjects)
            return;
        GenerateBlocks();
    }
    // Update is called once per frame
    void Update()
    {

    }
    protected void GenerateBlocks()
    {
        blockArray = new GameObject[width, height];
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Vector2 pos = startPos;
                pos.x += i * BlockSize.x;
                pos.y += j * BlockSize.y;
                blockArray[i, j] = Instantiate(blockObject, pos, transform.rotation);
                blockArray[i, j].transform.SetParent(transform);
                blockArray[i, j].GetComponent<Block>().indexes = new Vector2Int(i, j);
            }
        }
    }
    public static void DeleteAllBlocks()
    {
        foreach(var b in blockArray)
        {
            Destroy(b);
        }
    }
    public static Block GetBlock(Vector2Int indexes)
    {
        return GetBlock(indexes.x, indexes.y);
    }
    public static Block GetBlock(int x, int y)
    {
        return GetGameObject(x, y)?.GetComponent<Block>();
    }
    public static GameObject GetGameObject(int x, int y)
    {
        if (x < 0 || x >= width || y < 0 || y >= height)
            return null;
        return blockArray[x, y];
    }
    public static GameObject GetGameObjectBlock(Vector2 pos)
    {
        int layerMask = 1 << 10;
        RaycastHit2D hit = Physics2D.Raycast(pos,pos,Mathf.Infinity,layerMask);
        if (hit.collider != null)
        {
            return hit.collider.gameObject;
        }
        return null;
    }
    public static Block GetBlock(Vector2 pos)
    {
        return GetGameObjectBlock(pos)?.GetComponent<Block>();
    }

    public GameObject CreateBlock(ETypeOfBlock t)
    {
        GameObject res = Instantiate(typesOfBlocks[(int)t]);
        res.transform.SetParent(transform);
        return res;
    }
    public GameObject CreateBlock(ETypeOfBlock t, Vector2Int index)
    {
        if (blockArray[index.x, index.y] != null)
        {
            Destroy(blockArray[index.x, index.y]);
        }
        Vector2 pos = startPos;
        pos.x += index.x * BlockSize.x;
        pos.y += index.y * BlockSize.y;
        GameObject res = CreateBlock(t);
        res.transform.position = pos;
        res.GetComponent<Block>().indexes = index;
        blockArray[index.x,index.y] = res;
        return res;
    }

    public void ChangeTypeOfBlock(Block bl)
    {
        Vector2Int indexes = bl.indexes;
        CreateBlock(typeOfBlockToBuild, indexes);
    }
    public void SetTypeOfBlockToBuild(Dropdown dp)
    {
        owner.inputControl.TypeOfAction = InputControl.ETypeOfInputAction.changingBlockType;
        typeOfBlockToBuild=(ETypeOfBlock)dp.value;
    }
}
