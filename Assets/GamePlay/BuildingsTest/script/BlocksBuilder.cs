using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlocksBuilder : MonoBehaviour
{
    public static int widthOfMap = 60;
    public static int heightOfMap = 40;
    public static Vector2 startPosOfMap = new Vector2(0, 0);
    public static float radiusOfBlock = 1;
    public static float zPositionOfBlocks = 0;
    public static float sqr3 = Mathf.Sqrt(3);

    public static GameObject[,] arrayOfBlocks = new GameObject[widthOfMap, heightOfMap];
    public GameObject prefab;

    // Start is called before the first frame update
    void Start()
    {
        //DrawMapFromBlocksSquare();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //creating an array from blocks
    public void DrawMapFromBlocks(){
        for (int i=0;i<widthOfMap;i++){
            for (int j=0;j<heightOfMap;j++){
                Instantiate(prefab, new Vector2(radiusOfBlock * 3 / 2 * j, radiusOfBlock * i * sqr3 + (j%2==0?0:radiusOfBlock/2*sqr3)), transform.rotation);
            }
        }
    }

    //creating an array from blocks
    public void DrawMapFromBlocksSquare()
    {
        for (int i = 0; i < widthOfMap; i++)
        {
            for (int j = 0; j < heightOfMap; j++)
            {
                Instantiate(prefab, new Vector2(radiusOfBlock * i, radiusOfBlock * j), transform.rotation);
            }
        }
    }

    //creating a block at cell 
    public static void CreateBlockAtCell(GameObject cell){
        
    }
}
