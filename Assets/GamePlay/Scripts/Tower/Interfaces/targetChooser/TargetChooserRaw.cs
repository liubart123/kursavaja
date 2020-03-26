using Assets.GamePlay.Scripts.Enemies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.GamePlay.Scripts.Tower.Interfaces.targetChooser
{
    class TargetChooserRaw : TargetChooser
    {
        public Enemy targetFromUnityEditor;
        public override Enemy ChooseTarget(TargetChooserParameters args)
        {
            return targetFromUnityEditor;
        }
    }
}
