using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rougelee
{
    public class Sword : ShotProjectile
    {

        public static bool canMove = false;


        private void Awake()
        {
            movespeed = 0;
            damage = 20;
        }


        void FixedUpdate()
        {
            //if bullet gets too far away, it disappears
            if (Vector3.Distance(player.transform.position, transform.position) > 40)
            {
                Destroy(gameObject);
            }
        }

        //since the sword doesn't move, override the shoot method which gives the sword velocity
        //upgrades make the sword projectile move, so call the base method if upgrade attained
        public override void Shoot(Vector3 gunDirection)
        {
            if (canMove)
            {
                base.Shoot(gunDirection);
            }
            else
            {
                player = GameObject.FindGameObjectWithTag("Player");
                myAnim = GetComponent<Animator>();
            }
        }

        public void AnimationFinished()
        {
            if (!canMove)
            {
                Destroy(gameObject);
            }
        }

    }
}