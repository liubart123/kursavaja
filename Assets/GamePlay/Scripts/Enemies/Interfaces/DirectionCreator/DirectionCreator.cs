using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.GamePlay.Scripts.Enemies.Interfaces.DirectionCreator
{
    public abstract class DirectionCreator : MonoBehaviour
    {
        public abstract Vector2 CreateDirection(DirectionCreatorParameters args);
    }
    public class DirectionCreatorParameters
    {
        public ICollection<Block> path;
        public Vector2 enemyPos;

        public DirectionCreatorParameters(ICollection<Block> path, Vector2 enemyPos)
        {
            this.path = path;
            this.enemyPos = enemyPos;
        }
    }
}
