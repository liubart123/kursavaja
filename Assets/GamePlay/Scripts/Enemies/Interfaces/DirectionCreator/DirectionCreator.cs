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
        public Building.Building target;

    }
}
