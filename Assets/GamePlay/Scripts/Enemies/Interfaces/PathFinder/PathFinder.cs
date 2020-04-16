using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.GamePlay.Scripts.Enemies.Interfaces.PathFinder
{
    public class PathFinder
    {
        //public int counter = 0;
        //public float maxCounter = 300;
        private ElementOfPath[,] elementsOfPath = new ElementOfPath[BlocksGenerator.width, BlocksGenerator.height];
        public ICollection<Block> GetPath(Block start, Block finish)
        {
            List<ElementOfPath> reachable = new List<ElementOfPath>();
            reachable.Add(new ElementOfPath(null,start, 0));
            ElementOfPath finishEl = new ElementOfPath(null, finish, Mathf.Infinity);

            while (reachable.Count != 0)
            {
                //counter++;
                ElementOfPath next = ChooseNextElement(reachable, finishEl);
                next.isChecked = true;
                //if (counter < maxCounter)
                //{
                //    next.block.GetComponent<SpriteRenderer>().color = new Color(counter / maxCounter, 0, 0);
                //}else if (counter < maxCounter*2)
                //{
                //    next.block.GetComponent<SpriteRenderer>().color = new Color(1, (counter- maxCounter) / maxCounter, 0);
                //}else {
                //    next.block.GetComponent<SpriteRenderer>().color = new Color(1, 1, (counter -2* maxCounter )/ maxCounter);

                //}
                reachable.Remove(next);


                if (next.block.indexes.Equals(finish.indexes))
                {
                    return GetPath(next);
                }
                var neighbours = GetNeighbours(next);
                foreach(var neighbour in neighbours)
                {
                    reachable.Add(neighbour);
                }
            }
            return null;
        }
        private class ElementOfPath
        {
            public ElementOfPath prev;
            public Block block;
            public float cost;
            public float passabilty;
            public bool isChecked = false;

            public ElementOfPath(ElementOfPath prev, Block block, float cost)
            {
                this.prev = prev;
                this.block = block;
                this.cost = cost;
            }
        }
        private ElementOfPath ChooseNextElement(ICollection<ElementOfPath> reachable, ElementOfPath finish)
        {
            float minCost = Mathf.Infinity;
            ElementOfPath result = null;
            float costToGoal, totalCost;
            foreach (var block in reachable)
            {
                costToGoal = GetPossibleCost(block, finish);
                totalCost = costToGoal + block.cost;
                if (totalCost < minCost)
                {
                    minCost = totalCost;
                    result = block;
                }
            }
            return result;
        }
        private static float averageCostOfBlock = 1;
        private float GetPossibleCost(ElementOfPath start, ElementOfPath finish)
        {
            int countOfBlock = Mathf.Abs(start.block.indexes.x - finish.block.indexes.x);
            countOfBlock += Mathf.Abs(start.block.indexes.y - finish.block.indexes.y);
            return countOfBlock * averageCostOfBlock;
        }
        private ICollection<Block> GetPath(ElementOfPath last)
        {
            List<Block> result = new List<Block>();

            ElementOfPath temp = last;
            while (temp.prev != null)
            {
                result.Add(temp.block);
                temp = temp.prev;
            }
            result.Add(temp.block);

            return result;
        }
        //атрымаць суседаў блёка, якія яшчэ не правераныя, і змяніць іх кошт
        private ICollection<ElementOfPath> GetNeighbours(ElementOfPath el)
        {
            List<ElementOfPath> result = new List<ElementOfPath>();
            Vector2Int currentPos = el.block.indexes;
            for (float angle = 0; angle < Mathf.PI * 2; angle += Mathf.PI / 2)
            {
                int x, y;
                int test = (int)Mathf.Sin(Mathf.PI/2);
                x = currentPos.x + (int)Mathf.Cos(angle);
                y = currentPos.y + (int)Mathf.Sin(angle);
                if (x < 0 || x >= BlocksGenerator.width || y < 0 || y >= BlocksGenerator.height)
                    continue;
                //знахожу суседа
                ElementOfPath temp = elementsOfPath[x, y];
                if (temp.isChecked)
                {
                    continue;   //калі сусед правераны, то прапускаю
                }
                float newCost = el.cost + temp.passabilty;  //вылічваю новы магчымы шлях
                if (newCost < temp.cost)
                {
                    temp.cost = newCost;
                    temp.prev = el;
                    result.Add(temp);
                }
            }
            return result;
        }
        public PathFinder()
        {
            GenerateElementsArray();
        }
        private void GenerateElementsArray()
        {
            for (int i = 0; i < BlocksGenerator.width; i++)
            {
                for (int j = 0; j < BlocksGenerator.height; j++)
                {
                    elementsOfPath[i,j] = new ElementOfPath(null, BlocksGenerator.GetBlock(i, j), Mathf.Infinity);
                    elementsOfPath[i, j].passabilty = BlocksGenerator.GetBlock(i, j).passability;
                    //elementsOfPath[i, j].block.GetComponent<SpriteRenderer>().color = Color.white;
                }
            }
        }
    }
}
