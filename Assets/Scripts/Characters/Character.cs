using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rougelee
{
    public class Character : MonoBehaviour
    {

        public GameObject[] projectileArray;
        public GameObject[] upgradeArray;
        public bool facingLeft = false;
        public bool startRunning = false;
        protected bool isRunning = false;
        protected bool isAttacking = false;
        public Gun[] weapons = new Gun[2];
        protected SpriteRenderer sp;
        protected Animator myAnim;

        protected GameObject crosshair;

        private void Awake()
        {
            projectileArray[0].GetComponent<ShotProjectile>().Reset();
            weapons[0] = new Gun(projectileArray[0]);
            weapons[1] = new Gun(projectileArray[1]);
            sp = GetComponent<SpriteRenderer>();
            myAnim = GetComponent<Animator>();
            crosshair = transform.parent.GetChild(3).gameObject;
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void FixedUpdate()
        {
            
        }

 

        public virtual bool UseWeapon(int weapon)
        {
            bool shot = false;
            if (weapons[weapon] != null)
            {
                shot = weapons[weapon].Shoot();
            }
            return shot;
        }

        public void IsAttacking(int isAttacking)
        {
            this.isAttacking = isAttacking==1;
        }

    }
}