using Assets.GamePlay.Scripts.GUI;
using Assets.GamePlay.Scripts.Mechanic;
using Assets.GamePlay.Scripts.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuiControl : MonoBehaviour
{
    public GameObject menuPanel;
    public GameObject viewControllPanel;
    public BuildingsInfoManager towerInfoManager;
    public GameObject towerCombinationPanel;
    public GameObject onlineSettingsPanel;
    public MyPlayer owner;
    public void Initialize(MyPlayer pl)
    {
        owner = pl;
        towerInfoManager?.Initialize(pl);
    }
    private void Start()
    {
        //towerInfoManager = FindObjectOfType<BuildingsInfoManager>();
    }
    public void OpenMenuPanel()
    {
        CloseAllPanels();
        menuPanel.SetActive(true);
    }
    public void OpenCombinationPanel()
    {
        CloseAllPanels();
        towerCombinationPanel.SetActive(true);
    }
    public void OpenViewControllPanel()
    {
        CloseAllPanels();
        viewControllPanel.SetActive(true);
    }
    public void OpenOnlineSettingsPanel()
    {
        CloseAllPanels();
        onlineSettingsPanel.SetActive(true);
    }
    public void CloseAllPanels()
    {


        if (towerInfoManager == null)
            return;
        towerInfoManager?.ClosePanels();
        CloseMenuPanel();
        CloseViewManagerPanel();
    }
    public void CloseMenuPanel()
    {

        if (menuPanel == null)
            return;
        menuPanel?.SetActive(false);
    }
    public void CloseViewManagerPanel()
    {
        if (viewControllPanel == null)
            return;
        viewControllPanel?.SetActive(false);
    }
    public void CloseCombinationPanel()
    {
        if (towerCombinationPanel == null)
            return;
        towerCombinationPanel?.SetActive(false);
    }
    public void CloseOnlineSettingsPanel()
    {
        onlineSettingsPanel?.SetActive(false);
    }
}
