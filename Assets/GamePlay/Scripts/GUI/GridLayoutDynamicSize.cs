using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.GamePlay.Scripts.GUI
{
    public class GridLayoutDynamicSize : MonoBehaviour
    {
        float oldHeight;
        public int tableSize;
        public float cellToSpace;   //адносіна ячэйкі да месца паміж ячэйкамі
        private void Update()
        {
            float newHeight = GetComponent<RectTransform>().rect.height;
            if (oldHeight != newHeight)
            {
                oldHeight = newHeight;
                float cellW = newHeight / (tableSize + tableSize * cellToSpace + cellToSpace);
                float spaceW = cellW * cellToSpace;
                GetComponent<GridLayoutGroup>().cellSize =
                    new Vector2(cellW, cellW);
                GetComponent<GridLayoutGroup>().spacing =
                    new Vector2(spaceW, spaceW);
            }
        }
        private void Awake()
        {
            float newHeight = GetComponent<RectTransform>().rect.height;
            if (oldHeight != newHeight)
            {
                oldHeight = newHeight;
                float cellW = newHeight / (tableSize + tableSize * cellToSpace + cellToSpace);
                float spaceW = cellW * cellToSpace;
                GetComponent<GridLayoutGroup>().cellSize =
                    new Vector2(cellW, cellW);
                GetComponent<GridLayoutGroup>().spacing =
                    new Vector2(spaceW, spaceW);
            }
        }
    }
}
