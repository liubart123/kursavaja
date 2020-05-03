using Assets.GamePlay.Scripts.Building;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Block : MonoBehaviour
{
    public float passability;   //рыяльнае значэнне
    public float passabilityFromUnity;  //значэнне па змаўчанні
    public Vector2Int indexes;
    public bool isBasement;
    public ETypeOfBlock typeOfBlock;
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
    public void ResetPassability()
    {
        passability = passabilityFromUnity;
    }
    public enum ETypeOfBlock { 
        normal,
        basement
    }

    private void Start()
    {
        ResetPassability();
    }
}
