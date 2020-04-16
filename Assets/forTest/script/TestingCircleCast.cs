using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.forTest.script
{
    public class TestingCircleCast : MonoBehaviour
    {
        Camera camera;
        private void Start()
        {
            camera = FindObjectOfType<Camera>();
        }
        public float radius;
        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                int layerMask = 1 << 13;
                RaycastHit2D hit = Physics2D.CircleCast(
                    camera.ScreenToWorldPoint(Input.mousePosition), radius,
                    Vector2.zero, Mathf.Infinity,
                    layerMask);
                //RaycastHit2D hit = Physics2D.Raycast(camera.ScreenToWorldPoint(Input.mousePosition), camera.ScreenToWorldPoint(Input.mousePosition), Mathf.Infinity, layerMask);
                if (hit.collider != null)
                {
                    Debug.Log(hit.collider);
                }
            }
        }
    }
}
