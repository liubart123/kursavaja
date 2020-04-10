using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlocksGenerator : MonoBehaviour
{
    public int width;
    public int height;
    public GameObject blockObject;
    public GameObject[,] blockArray;
    public Vector2 startPos;
    public Vector2 BlockSize;
    // Start is called before the first frame update
    void Start()
    {
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
            }
        }
    }
}
