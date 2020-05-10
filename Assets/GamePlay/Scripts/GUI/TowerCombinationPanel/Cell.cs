using Assets.GamePlay.Scripts.BulletEffects;
using Assets.GamePlay.Scripts.TowerClasses;
using Assets.GamePlay.Scripts.TowerClasses.TowerCombinations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static Assets.GamePlay.Scripts.BulletEffects.BulletEffect;
using static Assets.GamePlay.Scripts.Damage.DamageManager;
using static Assets.GamePlay.Scripts.TowerClasses.TowerClasseGenerator;

namespace Assets.GamePlay.Scripts.GUI.TowerCombinationPanel
{
    public class Cell : MonoBehaviour
    {
        public Vector2Int indexes;
        public TowerClass towerClass;
        public ETowerClass typeOfClass;
        [SerializeField]
        public int idOfCombination = -1;
        public BulletEffect bulletEffect;

        public override string ToString()
        {
            if (towerClass != null)
            {
                return $"tower class: {towerClass.TowerClassName} \n" +
                    $"{towerClass.typeOfTower.ToString()}";
            }
            else if (bulletEffect != null)
            {
                string damage = "";
                if (bulletEffect is BulletEffectImmidiateDamageRaw)
                {
                    damage = "type of damage: " + (bulletEffect as BulletEffectImmidiateDamageRaw).kindOfDamage.ToString();
                }else if (bulletEffect is BulletEffectPeriodicDamageRaw)
                {
                    damage = "type of damage: " + (bulletEffect as BulletEffectPeriodicDamageRaw).kindOfDamage.ToString();
                }
                return $"effect: {bulletEffect.effectName} \n" +
                    $"value: {bulletEffect.effectivity}\n" +
                    $"{(damage!=""?damage:"" )}";
            }
            else
            {
                return "empty";
            }
        }
    }
    [Serializable]
    public class CellSerializable
    {
        [HideInInspector]
        public Vector2Int indexes;
        [HideInInspector]
        public ETowerClass typeOfClass;
        [HideInInspector]
        public bool isThereClass = false;
        [SerializeReference]
        public BulletEffect bulletEffect;
        [HideInInspector]
        public int idOfCombination;
        public CellSerializable(Cell cell)
        {
            indexes = cell.indexes;
            typeOfClass = cell.typeOfClass;
            bulletEffect = cell.bulletEffect;
            if (cell.towerClass != null)
            {
                isThereClass = true;
            }
            idOfCombination = cell.idOfCombination;
        }
    }
}
