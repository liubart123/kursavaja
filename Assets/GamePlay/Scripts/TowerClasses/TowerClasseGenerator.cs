using Assets.GamePlay.Scripts.BulletEffects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static Assets.GamePlay.Scripts.Damage.DamageManager;

namespace Assets.GamePlay.Scripts.TowerClasses
{
    //кляс, для генерацыі клясаў веж адпаведна параметрам. У кожнага гульца свой экзэмлпяр
    public class TowerClasseGenerator : MonoBehaviour
    {
        public ICollection<TowerClass> towerClasess;
        public TowerClass GetTowerClass(ETowerClass towerClasse)
        {
            return towerClasess.FirstOrDefault(el => towerClasse == el.typeOfTower);
        }

        private void Start()
        {
        }


        private float damage = 500;
        private float periodicDamage = 1;
        private float startSlow = 1.5f;
        public void Initialize()
        {
            towerClasess = new List<TowerClass>();
            //default classes
            towerClasess.Add(new TowerClass(ETowerClass.damageBlue, "α",
                new BulletEffectImmidiateDamageRaw(damage, EKindOfDamage.blue)
                ));
            towerClasess.Add(new TowerClass(ETowerClass.damageGreen, "β",
                new BulletEffectImmidiateDamageRaw(damage, EKindOfDamage.green)
                ));
            towerClasess.Add(new TowerClass(ETowerClass.damageRed, "γ",
                new BulletEffectImmidiateDamageRaw(damage, EKindOfDamage.red)
                ));
        }


        public enum ETowerClass
        {
            damageRed,
            damageGreen,
            damageBlue,
            damagePeriodicRed,
            damagePeriodicGreen,
            damagePeriodicBlue,
            slowerClass,
            combination
        }
    }
}
