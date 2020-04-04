using Assets.GamePlay.Scripts.Ammo;
using Assets.GamePlay.Scripts.Enemies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.GamePlay.Scripts.Tower.Interfaces
{
    public abstract class Shooter : MonoBehaviour
    {
        public abstract void Shoot(ShooterParameters args);
    }
    public class ShooterParameters
    {
        public Enemy target;
        public Bullet bullet;
        public Vector2 direction;

        public ShooterParameters(Enemy target, Bullet bullet)
        {
            this.target = target;
            this.bullet = bullet;
        }
        public ShooterParameters(Vector2 direction, Bullet bullet)
        {
            this.direction = direction;
            this.bullet = bullet;
        }
    }
}
