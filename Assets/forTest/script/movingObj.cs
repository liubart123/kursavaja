using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingObj : MonoBehaviour
{
    public float radiusOfMoving = 10;
    public Vector2 startPos = new Vector2(20,10);
    float degree = 0;
    public float angularSpeed = 100;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = startPos + new Vector2(Mathf.Cos(degree), Mathf.Sin(degree)) * radiusOfMoving;
        if (transform.parent != null)
            transform.position += transform.parent.position;
        if (degree>Mathf.PI*2){
            degree = 0;
        }
        degree += Mathf.PI / angularSpeed;
    }
}
