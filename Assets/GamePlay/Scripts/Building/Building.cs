using Assets.GamePlay.Scripts.Bonuses;
using Assets.GamePlay.Scripts.Building.interfaces.HealthContorller;
using Assets.GamePlay.Scripts.Enemies;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.GamePlay.Scripts.Building {
    public abstract class Building : MonoBehaviour
    {
        public Player.Player owner;
        public Builder.EBuilding typeOfBuilding;


        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
        public virtual void Initialize()
        {
            GetBlock().passability = 1;
        }

        //атрымаць блёк на якім пабудаваны будынак
        public Block GetBlock()
        {
            return transform.parent.gameObject.GetComponent<Block>();
        }

        public virtual void Die()
        {
            GetBlock().passability = 1;
            Destroy(this.gameObject);
        }
    }
}
