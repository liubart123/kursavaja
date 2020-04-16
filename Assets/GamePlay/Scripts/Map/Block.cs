using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Block : MonoBehaviour
{
    public float passability;
    public Vector2Int indexes;
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
    public Vector2 GetPosition()
    {
        return transform.position;
    }

}
