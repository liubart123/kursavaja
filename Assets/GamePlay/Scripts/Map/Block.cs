using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Block : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    public bool HasBuilding()
    {
        if (transform.childCount == 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

}
