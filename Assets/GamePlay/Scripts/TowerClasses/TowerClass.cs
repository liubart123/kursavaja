using Assets.GamePlay.Scripts.BulletEffects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.GamePlay.Scripts.TowerClasses
{
    public abstract class TowerClass
    {
        public string Name { get; set; }
        public ICollection<BulletEffect> BulletEffects { get; protected set; } 
        public int Id { get; protected set; }
        public TowerClass()
        {
            BulletEffects = new List<BulletEffect>();
        }
    }
}
