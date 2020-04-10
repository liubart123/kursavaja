using Assets.GamePlay.Scripts.Ammo;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingControll : MonoBehaviour
{
    //count of frames for living
    public int timeOfLive = 20;
    protected int currentTimeOfLive = 0;
    public void Live()
    {
        if (currentTimeOfLive++ > timeOfLive)
        {
            currentTimeOfLive = 0;
            GetComponent<Bullet>().Delete();
            //Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Live();
    }
}
