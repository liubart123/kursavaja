using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.GamePlay.Scripts.Enemies.Interfaces.MovingTargetChooser
{
    public abstract class EnemyMovingTargetChooser : MonoBehaviour
    {
        public virtual Building.Building ChooseTargetForMove(EnemyMovingTargetChooserParameters arg)
        {
            return null;
        }

    }
    public class EnemyMovingTargetChooserParameters
    {

    }
}
