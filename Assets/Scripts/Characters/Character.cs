using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rougelee
{
    public class Character : MonoBehaviour
    {

        public GameObject[] projectileArray;
        public bool facingLeft = false;
        public bool startRunning = false;
        protected bool isRunning = false;
        protected bool isAttacking = false;
        bool flipped = false;
        Gun[] weapons = new Gun[2];
        protected SpriteRenderer sp;
        protected Animator myAnim;

        private void Awake()
        {
            weapons[0] = new Gun(projectileArray[0]);
            weapons[1] = new Gun(projectileArray[1]);
            sp = GetComponent<SpriteRenderer>();
            myAnim = GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void FixedUpdate()
        {
            
        }

 

        public virtual void UseWeapon(int weapon)
        {
            weapons[weapon].Shoot();
        }

    }
}