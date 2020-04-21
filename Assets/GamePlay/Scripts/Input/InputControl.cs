using Assets.GamePlay.Scripts.Building;
using Assets.GamePlay.Scripts.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputControl : MonoBehaviour
{
    public Player owner;
    private CameraMove cameraMove;
    private Camera camera;
    public ETypeOfInputAction typeOfAction;
    public void SetTypeOfAction(string t)
    {
        if (t == ETypeOfInputAction.destroy.ToString())
        {
            typeOfAction = ETypeOfInputAction.destroy;
        }
    }
    public enum ETypeOfInputAction
    {
        nothing,
        build,
        destroy
    }
    // Start is called before the first frame update
    void Start()
    {
        cameraMove = GetComponent<CameraMove>();
        camera = FindObjectOfType<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        //wasd
        if (Input.GetAxis("Horizontal")!=0 || Input.GetAxis("Vertical")!=0){
            cameraMove.MoveCamera(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")));
        }
        //scrolling
        if (Input.mouseScrollDelta.y != 0){
            cameraMove.ScrollCamera(-(int)Mathf.Sign(Input.mouseScrollDelta.y));
        }
        EventSystem es = FindObjectOfType<EventSystem>();
        if (Input.GetMouseButtonDown(0))
        {
            //was this click on ui or gane object
            if (EventSystem.current.IsPointerOverGameObject())
            {

            } else
            {
                GameObject blockObj = BlocksGenerator.GetGameObjectBlock(camera.ScreenToWorldPoint(Input.mousePosition));
                if (blockObj != null)
                {
                    Block currentBlock = blockObj.GetComponent<Block>();
                    if (currentBlock != null)
                    {
                        if (typeOfAction == ETypeOfInputAction.build)
                        {
                            owner.builder.BuildBuildingOnBlock(currentBlock);
                        } else if (typeOfAction == ETypeOfInputAction.destroy) {
                            currentBlock.GetBuilding()?.GetComponent<Building>()?.Die();
                        }
                    }
                }
            }
        }
    }
}
