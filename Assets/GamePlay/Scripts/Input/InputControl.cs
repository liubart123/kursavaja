using Assets.GamePlay.Scripts.Building;
using Assets.GamePlay.Scripts.Player;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputControl : MonoBehaviour
{
    public Player owner;
    protected CameraMove cameraMove;
    protected new Camera camera;
    public ETypeOfInputAction typeOfAction;
    protected GuiControl guiControl;

    protected Action revertState;
    public void SetTypeOfAction(string t)
    {
        if (t == ETypeOfInputAction.destroy.ToString())
        {
            typeOfAction = ETypeOfInputAction.destroy;
        }
        else if (t == ETypeOfInputAction.creatingBonusConveyor.ToString())
        {
            typeOfAction = ETypeOfInputAction.creatingBonusConveyor;
        }
    }
    public enum ETypeOfInputAction
    {
        nothing,
        build,
        destroy,
        showTowerInfo,
        creatingBonusConveyor
    }
    // Start is called before the first frame update
    public virtual void Start()
    {
        cameraMove = GetComponent<CameraMove>();
        camera = FindObjectOfType<Camera>();
        guiControl = FindObjectOfType<GuiControl>();
    }

    // Update is called once per frame
    public virtual void Update()
    {
        //wasd
        if (Input.GetAxis("Horizontal")!=0 || Input.GetAxis("Vertical")!=0){
            cameraMove.MoveCamera(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")));
        }
        //scrolling
        if (Input.mouseScrollDelta.y != 0){
            cameraMove.ScrollCamera(-(int)Mathf.Sign(Input.mouseScrollDelta.y));
        }
        //EventSystem es = FindObjectOfType<EventSystem>();
        //if (Input.GetMouseButtonDown(0))
        //{
        //    //was this click on ui or gane object
        //    if (EventSystem.current.IsPointerOverGameObject())
        //    {

        //    } else
        //    {
        //        GameObject blockObj = BlocksGenerator.GetGameObjectBlock(camera.ScreenToWorldPoint(Input.mousePosition));
        //        if (blockObj != null)
        //        {
        //            Block currentBlock = blockObj.GetComponent<Block>();
        //            if (currentBlock != null)
        //            {
        //                if (typeOfAction == ETypeOfInputAction.build)
        //                {
        //                    owner.builder.BuildBuildingOnBlock(currentBlock);
        //                } else if (typeOfAction == ETypeOfInputAction.destroy) {
        //                    currentBlock.GetBuilding()?.GetComponent<Building>()?.Die();
        //                }
        //            }
        //        }
        //    }
        //}
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (typeOfAction == ETypeOfInputAction.nothing)
            {
                guiControl.OpenMenuPanel();
            } else
            {
                typeOfAction = ETypeOfInputAction.nothing;
            }
        }
    }
}
