using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.GamePlay.Scripts.Damage {
    public static class DamageManager
    {
        public static float CalculateDamage(float armor, float damage, EKindOfDamage kind)
        {
            return damage / armor;
        }
        public enum EKindOfDamage
        {
            red,
            green,
            blue
        }
    }
}

