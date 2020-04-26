using Assets.GamePlay.Scripts.Building;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Block : MonoBehaviour
{
    public float passability;
    public Vector2Int indexes;
    // Start is called before the first frame update
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
    public GameObject GetBuilding()
    {
        if (transform.childCount == 0)
        {
            return null;
        }
        return transform.GetChild(0).gameObject;
    }
    public Vector2 GetPosition()
    {
        return transform.position;
    }

    public Color defaultColor, lightedColor;
    public void LightBlockUp()
    {
        GetComponent<SpriteRenderer>().color = lightedColor;
    }
    public void UnLightBlock()
    {
        GetComponent<SpriteRenderer>().color = defaultColor;
    }

}
