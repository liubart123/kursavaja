using Assets.GamePlay.Scripts.Enemies;
using Assets.GamePlay.Scripts.Enemies.Interfaces.PathFinder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.forTest.script
{
    public class PathFinderTest : MonoBehaviour
    {
        Camera camera;
        public float maxPassAbility;
        public float newPassabilty;
        private void Start()
        {
            camera = FindObjectOfType<Camera>();
        }
        public Block start;
        public Block finish;
        ICollection<Block> path ;
        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit2D hit = Physics2D.Raycast(camera.ScreenToWorldPoint(Input.mousePosition), camera.ScreenToWorldPoint(Input.mousePosition));
                if (hit.collider != null)
                {
                    Block currentBlock = hit.collider.gameObject.GetComponent<Block>();
                    if (currentBlock != null)
                    {
                        if (Input.GetKey(KeyCode.LeftControl))
                        {
                            start = currentBlock;
                        } else if (Input.GetKey(KeyCode.LeftShift))
                        {
                            finish = currentBlock;
                        }
                        else if (Input.GetKey(KeyCode.X))
                        {
                            currentBlock.passability = newPassabilty;
                            DrawBlocks();
                        }

                    }

                }
            }
            if (Input.GetKey(KeyCode.Space))
            {
                //PathFinder pathFinder = new PathFinder();
                //ClearPath();
                //path = pathFinder.GetPath(start, finish);
                //foreach(var bl in path)
                //{
                //    bl.transform.localScale = new Vector3(0.5f, 0.5f);
                //}
                //DrawBlocks();
                var enemies = GameObject.FindObjectsOfType<Enemy>();
                foreach (var en in enemies)
                {
                    en.ChooseTargetForMoving();
                }
            }
        }
        private void ClearPath()
        {
            if (path == null)
                return;
            foreach(var bl in path)
            {
                bl.transform.localScale = new Vector3(1, 1);
            }
        }
        private void DrawBlocks()
        {
            for (int i = 0; i < BlocksGenerator.width; i++)
            {
                for (int j = 0; j < BlocksGenerator.height; j++)
                {
                    float pass = BlocksGenerator.GetBlock(i, j).passability;
                    float color = -pass / maxPassAbility + 1;
                    BlocksGenerator.GetGameObject(i, j).GetComponent<SpriteRenderer>().color =
                        new Color(color, color, color);
                }
            }
        }
    }
}
