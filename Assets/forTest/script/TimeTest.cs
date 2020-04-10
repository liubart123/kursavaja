using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class TimeTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int num = 0;
        TimerCallback tc = new TimerCallback((object obj) =>
        {
            Debug.Log(obj);
        });
        Timer timer = new Timer(tc, num, 4000, -1);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Time.timeScale = 0;
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Time.timeScale = 1;
        }
    }
}
