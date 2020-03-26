using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotating : MonoBehaviour
{
    public float angularSpeed = 0.01f;
    private float currentZ = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentZ += angularSpeed;
        transform.rotation = Quaternion.Euler(0, 0, currentZ);
    }
}
