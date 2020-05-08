using Assets.GamePlay.Scripts.Bonuses;
using Assets.GamePlay.Scripts.Building;
using Assets.GamePlay.Scripts.GUI;
using Assets.GamePlay.Scripts.Player;
using Assets.GamePlay.Scripts.Tower;
using Assets.GamePlay.Scripts.Waves;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputControlPlay : InputControl
{
    public BuildingsInfoManager towerInfoManager;

    public override void Start()
    {
        base.Start();
        //towerInfoManager = FindObjectOfType<TowerInfoManager>();
    }
    // Update is called once per frame
    public void CreateNewBonusConveyor()
    {
        TypeOfAction = ETypeOfInputAction.creatingBonusConveyor;
        prevTower?.bonusConveyor.ResetConveyor();
        ShowTowerInfo(prevTower);
    }
    protected Tower prevTower;

    public override void Initialize(MyPlayer pl)
    {
        base.Initialize(pl);
        towerInfoManager = pl.guiControl.towerInfoManager;
    }
    public override void Update()
    {
        base.Update();
        if (Input.GetMouseButtonDown(0) && WaveManager.IsThereWave==false)
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
                        if (TypeOfAction == ETypeOfInputAction.build && !currentBlock.HasBuilding())
                        {
                            CleareScreen();
                            owner.builder.BuildBuildingOnBlock(currentBlock);
                        } else if (TypeOfAction == ETypeOfInputAction.destroy)
                        {
                            CleareScreen();
                            Building building = currentBlock.GetBuilding()?.GetComponent<Building>();
                            if (building != null)
                            {
                                owner.builder.DestroyBuilding(building);
                                //building.Die();
                            }
                        }
                        else /*(typeOfAction == ETypeOfInputAction.showTowerInfo)*/
                        {
                            Building b = currentBlock.GetBuilding()?.GetComponent<Building>();
                            if (b != null)
                            {
                                if (b is Tower)
                                {
                                    CleareScreen();
                                    ShowTowerInfo(b as Tower);
                                } else if (b is Bonus)
                                {
                                    if (TypeOfAction == ETypeOfInputAction.creatingBonusConveyor)
                                    {
                                        prevTower.bonusConveyor.AddBonus(b as Bonus);
                                        ShowTowerInfo(prevTower);
                                    } else
                                    {
                                        CleareScreen();
                                        towerInfoManager.ShowBonusInfo(b as Bonus);
                                    }
                                }
                            }  else
                            {
                                CleareScreen();
                            } 
                        }
                    }
                }
            }
        }
    }

    private void ShowTowerInfo(Tower b)
    {
        towerInfoManager.ShowTowerInfo(b as Tower);
        prevTower = b as Tower;
        prevTower.ShowTowerInfo();
        revertState += () => prevTower?.HideTowerInfo();

    }

    //закрыццё вакенцаў і дадатковай інфы - чысты экран
    private void CleareScreen()
    {
        revertState?.Invoke();
        revertState = null;
    }
}
