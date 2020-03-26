using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Experimental.Rendering.LWRP;
using UnityEngine.Experimental.Rendering.Universal;

public class AutoStuffGeneration : MonoBehaviour
{
    public int countOfObjects;
    public List<GameObject> possibleObjects;
    public Vector3 startPos;
    public Vector3 endPos;
    // Start is called before the first frame update
    void Start()
    {
        Generate();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Generate(){
        for (int i=0;i<countOfObjects;i++){
            int objIndex = Random.Range(0, possibleObjects.Count);
            Vector3 pos = new Vector3(
                    Random.Range(startPos.x, endPos.x),
                    Random.Range(startPos.y, endPos.y),
                    Random.Range(startPos.z, endPos.z));
            GameObject temp = Instantiate(possibleObjects[objIndex], pos, transform.rotation);
            temp.transform.SetParent(transform);
            float red = Random.Range(0, 1f);
            float green = Random.Range(0, 1 - red);
            float blue = 1 - red - green;
            //temp.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(red, green, blue);

            temp.transform.GetChild(0).gameObject.GetComponent<Rotating>().angularSpeed = Random.Range(0.1f, 0.5f);
            temp.transform.GetChild(1).gameObject.GetComponent<movingObj>().angularSpeed = Random.Range(80, 200);
            //temp.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(
            //        Random.Range(0, 1f),
            //        Random.Range(0, 1f),
            //        Random.Range(0, 1f));
            //temp.transform.GetChild(1).GetComponent<Light2D>().color = new Color(
            //        Random.Range(0, 1f),
            //        Random.Range(0, 1f),
            //        Random.Range(0, 1f));
        }
    }
}
