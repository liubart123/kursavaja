using Assets.GamePlay.Scripts.BulletEffects;
using Assets.GamePlay.Scripts.Damage;
using Assets.GamePlay.Scripts.Enemies.Interfaces;
using Assets.GamePlay.Scripts.Enemies.Interfaces.Damager;
using Assets.GamePlay.Scripts.Enemies.Interfaces.DirectionCreator;
using Assets.GamePlay.Scripts.Enemies.Interfaces.MovingTargetChooser;
using Assets.GamePlay.Scripts.Enemies.Interfaces.PathFinder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static Assets.GamePlay.Scripts.Damage.DamageManager;
using Assets.GamePlay.Scripts.Building;

namespace Assets.GamePlay.Scripts.Enemies
{
    public abstract class Enemy : MonoBehaviour
    {
        public event Action<Enemy> eventsWhenThisDie;

        protected ICollection<Block> currentPath;

        //CHOOSING TARGET
        protected Building.Building targetToMove;   
        public EnemyMovingTargetChooser EnemyMovingTargetChooser { get; protected set; }
        public virtual void ChooseTargetForMoving()
        {
            if (targetToMove != null)
            {
                //Debug.Log(this.gameObject.name + " removing event after death from " + targetToMove.gameObject.name);
                targetToMove.OnDying -= ChooseTargetForMovingAfterTargetsDeath;
            }
            targetToMove = EnemyMovingTargetChooser.ChooseTargetForMove(
                new EnemyMovingTargetChooserParameters(GetPosition()));
            CreatePath();
            if (targetToMove != null)
            {
                //Debug.Log(this.gameObject.name + " adding event after death from " + targetToMove.gameObject.name);
                targetToMove.OnDying += ChooseTargetForMovingAfterTargetsDeath;
            }
        }
        private void ChooseTargetForMovingAfterTargetsDeath(Building.Building b)
        {
            //Debug.Log("choosing new target for moving after target death");
            if (targetToMove == b)
            {
                targetToMove.OnDying -= ChooseTargetForMovingAfterTargetsDeath;
                targetToMove = null;
                ChooseTargetForMoving();
            }
        }

        //MOVING
        public float speed;

        [SerializeField]
        protected Vector2 currentDirection;
        public DirectionCreator DirectionCreator { get; protected set; }
        protected virtual void CreateDirection()
        {
            currentDirection = DirectionCreator.CreateDirection(
                new DirectionCreatorParameters(currentPath,
                GetPosition()));
        }
        protected virtual void CreatePath()
        {
            if (targetToMove != null)
            {
                PathFinder pf = new PathFinder();
                currentPath = pf.GetPath(
                    BlocksGenerator.GetBlock(GetPosition()),
                    targetToMove.GetBlock());
            } else
            {
                currentPath = null;
            }
        }
        public virtual void Move()
        {
            GetComponent<Rigidbody2D>().velocity = currentDirection * speed;
        }


        //EFFECTS
        protected List<BulletEffect> listOfEffects = new List<BulletEffect>();
        public virtual void RecieveEffect(BulletEffect effect)
        {
            BulletEffect sameEffect = listOfEffects.FirstOrDefault<BulletEffect>(
                el =>
                {
                    return el.Equals(effect);
                });
            if (sameEffect != null)
            {
                if (sameEffect.Effectivity < effect.Effectivity)
                {
                    RemoveEffect(sameEffect);
                } else
                {
                    return;
                }
            }
            listOfEffects.Add(effect);
            effect.AffectOnce(this);
        }
        public virtual void RemoveEffect(BulletEffect effect)
        {
            listOfEffects.Remove(effect);
            effect.RemoveEffect(this);
        }


        //DAMAGE AND HEALTH
        [SerializeField]
        protected float maxHealth;
        protected float health;
        public float Health {
            get { return health; }
            set {
                health = value;
                ChangeViewAfterHealthChanging();
            } 
        }
        [SerializeField]
        protected float[] Resistance;
        public virtual void GetDamage(float damage, EKindOfDamage type)
        {
            Health -= DamageManager.CalculateDamage(Resistance[(int)type], damage, type);
            if (Health < 0)
            {
                Die();
            }
        }


        //CHANGING OF APPEARANCE DEPEND OF HEALTH
        //object that must change when health is changed
        protected GameObject[] childsOfGameobject;
        protected Color[] colorsOfChilds;
        protected void SetChildsOfGameobjects()
        {
            childsOfGameobject = new GameObject[transform.childCount];
            colorsOfChilds = new Color[transform.childCount];
            for (int i=0;i< transform.childCount; i++)
            {
                childsOfGameobject[i] = transform.GetChild(i).gameObject;
                colorsOfChilds[i] = transform.GetChild(i).gameObject.GetComponent<SpriteRenderer>().color;
            }
        }
        protected void ChangeViewAfterHealthChanging()
        {
            for (int i=0;i< childsOfGameobject.Length; i++)
            {
                float x = Health / maxHealth;
                x += 0.2f;
                if (x > 1) x = 1;
                childsOfGameobject[i].GetComponent<SpriteRenderer>().color
                    = Color.Lerp(Color.black, colorsOfChilds[i], x);
            }
        }


        //POSITION ROTATION
        public virtual Vector2 GetPosition()
        {
            return (Vector2)transform.position;
        }
        public virtual Vector2 GetCurrentDirectionOfMoving()
        {
            return currentDirection;
        }
        public virtual void FixedUpdate()
        {
            CreateDirection();
            Move();
            listOfEffects.ForEach(el => el.Affect(this));
        }
        protected virtual void Start()
        {
        }


        protected DamageMaker damageMaker;
        //DOING DAMAGE
        public void DoDamage(Building.Building building)
        {
            damageMaker.DoDamage(new DamageMakerPatameters(building));
            Die();
        }

        public virtual void Die()
        {
            eventsWhenThisDie?.Invoke(this);
            if (targetToMove != null && targetToMove.gameObject != null)
            {
                targetToMove.OnDying -= ChooseTargetForMovingAfterTargetsDeath;
            }
            Destroy(gameObject);
        }
        public virtual void Initialize()
        {
            //health
            SetChildsOfGameobjects();
            Health = maxHealth;

            //interfaces
            damageMaker = GetComponent<DamageMaker>();
            DirectionCreator = GetComponent<DirectionCreator>();
            EnemyMovingTargetChooser = GetComponent<EnemyMovingTargetChooser>();


            ChooseTargetForMoving();
        }
    }
}
