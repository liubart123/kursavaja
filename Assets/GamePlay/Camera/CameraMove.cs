using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class CameraMove : MonoBehaviour
{
    public float scrollSpeed;
    public  float moveSpeed = 1;
    private new Camera camera;
    Volume volume;
    UnityEngine.Rendering.Universal.Bloom bloom;
    // Start is called before the first frame update
    void Start()
    {
        camera = GetComponent<Camera>();
        volume = GetComponent<Volume>();
        if (volume.profile.components[0] is UnityEngine.Rendering.Universal.Bloom)
        {
            //bloom = volume.profile.components[0] as UnityEngine.Rendering.Universal.Bloom;
        }
    }

    public void ScrollCamera(int sign){
        camera.orthographicSize += sign * scrollSpeed;
        moveSpeed = camera.orthographicSize * 0.01f + 0.01f;
        //bloom.intensity.value = 1f + camera.orthographicSize * 0.1f;
        if (camera.orthographicSize<1){
            camera.orthographicSize = 1;
        }
    }
    public void MoveCamera(Vector2 dir){
        transform.position += new Vector3(dir.x * moveSpeed, dir.y * moveSpeed,0);
    }

}
