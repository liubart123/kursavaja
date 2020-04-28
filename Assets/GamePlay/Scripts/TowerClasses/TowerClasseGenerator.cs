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
        public TowerClass GetTowerClass(string towerClassName)
        {
            return towerClasess.FirstOrDefault(el => towerClassName == el.TowerClassName);
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
            towerClasess.Add(new TowerClass(ETowerClass.damageRed, "α", 
                new Color(0.8f, 0.4f, 0.4f),
                new EBonusType[] {
                    EBonusType.red,EBonusType.red,EBonusType.red },
                new BulletEffectImmidiateDamageRaw(damage, EKindOfDamage.red)
                ));
            towerClasess.Add(new TowerClass(ETowerClass.damageGreen, "β", 
                new Color(0.4f,0.8f,0.4f),
                new EBonusType[] {
                    EBonusType.green,EBonusType.green,EBonusType.green },
                new BulletEffectImmidiateDamageRaw(damage, EKindOfDamage.green)
                ));
            towerClasess.Add(new TowerClass(ETowerClass.damageBlue, "γ",
                new Color(0.4f, 0.4f, 0.8f),
                new EBonusType[] {
                    EBonusType.blue,EBonusType.blue,EBonusType.blue },
                new BulletEffectImmidiateDamageRaw(damage, EKindOfDamage.blue)
                ));

            towerClasess.Add(new TowerClass(ETowerClass.damageRedGreen, "ε",
                new Color(0.9f, 0.7f, 0.4f),
                new EBonusType[] {
                    EBonusType.red,EBonusType.green,EBonusType.red },
                new BulletEffectImmidiateDamageRaw(damage, EKindOfDamage.red),
                new BulletEffectImmidiateDamageRaw(damage/2, EKindOfDamage.green)
                ));
            towerClasess.Add(new TowerClass(ETowerClass.damageRedBlue, "ζ",
                new Color(0.9f, 0.4f, 0.7f),
                new EBonusType[] {
                    EBonusType.red,EBonusType.red,EBonusType.blue },
                new BulletEffectImmidiateDamageRaw(damage, EKindOfDamage.red),
                new BulletEffectImmidiateDamageRaw(damage / 2, EKindOfDamage.blue)
                ));

            towerClasess.Add(new TowerClass(ETowerClass.damageGreenRed, "η",
                new Color(0.7f, 0.9f, 0.4f),
                new EBonusType[] {
                    EBonusType.green,EBonusType.green,EBonusType.red },
                new BulletEffectImmidiateDamageRaw(damage, EKindOfDamage.green),
                new BulletEffectImmidiateDamageRaw(damage / 2, EKindOfDamage.red)
                ));
            towerClasess.Add(new TowerClass(ETowerClass.damageGreenBlue, "θ",
                new Color(0.4f, 0.9f, 0.7f),
                new EBonusType[] {
                    EBonusType.green,EBonusType.green,EBonusType.blue },
                new BulletEffectImmidiateDamageRaw(damage, EKindOfDamage.green),
                new BulletEffectImmidiateDamageRaw(damage / 2, EKindOfDamage.blue)
                ));

            towerClasess.Add(new TowerClass(ETowerClass.damageBlueRed, "ι",
                new Color(0.7f, 0.4f, 0.9f),
                new EBonusType[] {
                    EBonusType.blue,EBonusType.blue,EBonusType.red },
                new BulletEffectImmidiateDamageRaw(damage, EKindOfDamage.blue),
                new BulletEffectImmidiateDamageRaw(damage / 2, EKindOfDamage.red)
                ));
            towerClasess.Add(new TowerClass(ETowerClass.damageBlueGreen, "λ",
                new Color(0.4f, 0.7f, 0.9f),
                new EBonusType[] {
                    EBonusType.blue,EBonusType.blue,EBonusType.green },
                new BulletEffectImmidiateDamageRaw(damage, EKindOfDamage.blue),
                new BulletEffectImmidiateDamageRaw(damage / 2, EKindOfDamage.green)
                ));
            towerClasess.Add(new TowerClass(ETowerClass.slowerClass, "δ",
                new Color(0.8f, 0.8f, 0.8f),
                new EBonusType[] {
                    EBonusType.red,EBonusType.green,EBonusType.blue },
                new BulletEffectSlowingRaw(startSlow)
                ));
        }

        public TowerClass GetClassFromBonuses(BonusConveyor bonusConveyor)
        {
            EBonusType[] bonuses = new EBonusType[bonusConveyor.bonuses.Count];
            for (int i = 0; i < bonuses.Length; i++)
            {
                bonuses[i] = bonusConveyor.bonuses.ElementAt(i).bonusType;
            }
            foreach(var cl in towerClasess)
            {
                bool res = true;
                var tempBonusCollectionOfClass = cl.bonusesForClass.Clone() as EBonusType[];
                foreach (var b in bonuses)
                {
                    for (int i=0;i< tempBonusCollectionOfClass.Length; i++)
                    {
                        var el = tempBonusCollectionOfClass[i];
                        if (el == b)
                        {
                            tempBonusCollectionOfClass[i] = EBonusType.neutral;
                            el = EBonusType.neutral;
                            break;
                        }
                    }
                }
                for (int i = 0; i < tempBonusCollectionOfClass.Length; i++)
                {
                    if (tempBonusCollectionOfClass[i] != EBonusType.neutral)
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
            damageRedGreen,
            damageRedBlue,
            damageGreenRed,
            damageGreenBlue,
            damageBlueRed,
            damageBlueGreen,
            slowerClass,
            combination
        }
    }
}
