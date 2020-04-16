using Assets.GamePlay.Scripts.Bonuses;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.GamePlay.Scripts.Building {
    public abstract class Building : MonoBehaviour
    {
        public Player.Player owner;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
        public abstract void Initialize();
        public Block GetBlock()
        {
            return transform.parent.gameObject.GetComponent<Block>();
        }
    }
}
