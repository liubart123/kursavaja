using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestVelocity2D : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject testObj;
    void Start()
    {
        testObj.GetComponent<Rigidbody2D>().velocity = new Vector2(10, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
