using Assets.GamePlay.Scripts.Bonuses;
using Assets.GamePlay.Scripts.BulletEffects;
using Assets.GamePlay.Scripts.Tower.Interfaces.BonusConveyor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static Assets.GamePlay.Scripts.Bonuses.Bonus;
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
            towerClasess.Add(new TowerClass(ETowerClass.damageBlue, "α", new EBonusType[] { 
                    EBonusType.blue,EBonusType.blue,EBonusType.blue },
                new BulletEffectImmidiateDamageRaw(damage, EKindOfDamage.blue)
                ));
            towerClasess.Add(new TowerClass(ETowerClass.damageGreen, "β", new EBonusType[] {
                    EBonusType.green,EBonusType.green,EBonusType.green },
                new BulletEffectImmidiateDamageRaw(damage, EKindOfDamage.green)
                ));
            towerClasess.Add(new TowerClass(ETowerClass.damageRed, "γ", new EBonusType[] {
                    EBonusType.red,EBonusType.red,EBonusType.red },
                new BulletEffectImmidiateDamageRaw(damage, EKindOfDamage.red)
                ));
            towerClasess.Add(new TowerClass(ETowerClass.slowerClass, "γ", new EBonusType[] {
                    EBonusType.red,EBonusType.green,EBonusType.blue },
                new BulletEffectSlowingRaw(startSlow)
                ));
        }

        public TowerClass GetClassFromBonuses(BonusConveyor bonusConveyor)
        {
            EBonusType[] bonuses = new EBonusType[3];
            for (int i = 0; i < 3; i++)
            {
                bonuses[i] = bonusConveyor.bonuses.ElementAt(i).bonusType;
            }
            foreach(var cl in towerClasess)
            {
                bool res = true;
                foreach(var b in bonuses)
                {
                    if (!cl.bonusesForClass.Contains(b))
                    {
                        res = false;
                        break;
                    }
                }
                if (res)
                {
                    return cl;
                }
            }
            return null;
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
