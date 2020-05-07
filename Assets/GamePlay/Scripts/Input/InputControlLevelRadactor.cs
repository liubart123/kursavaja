using Assets.GamePlay.Scripts.Building;
using Assets.GamePlay.Scripts.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputControlLevelRadactor : InputControl
{
    
    // Update is called once per frame
    public override void Update()
    {
        base.Update();
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
                        if (TypeOfAction == ETypeOfInputAction.build)
                        {
                            owner.builder.BuildBuildingOnBlock(currentBlock);
                        } else if (TypeOfAction == ETypeOfInputAction.destroy) {
                            currentBlock.GetBuilding()?.GetComponent<Building>()?.Die();
                        }
                        else if (TypeOfAction == ETypeOfInputAction.changingBlockType)
                        {
                            currentBlock.GetBuilding()?.GetComponent<Building>()?.Die();
                            owner.blocksGenerator.ChangeTypeOfBlock(currentBlock);
                        }
                    }
                }
            }
        }
    }
}
