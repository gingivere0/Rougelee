using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rougelee
{
    public class Spike : ShotProjectile
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

        //since the spike doesn't move, override the shoot method which gives the spike velocity
        //upgrades make the spike projectile move, so call the base method if upgrade attained
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

            if (gunDirection.x < 0)
            {
                //GetComponent<SpriteRenderer>().flipY = true;
                Vector2 flipx = new Vector2(gameObject.transform.localScale.x, gameObject.transform.localScale.y*-1);
                gameObject.transform.localScale = flipx;
            }
        }

        public void AnimationFinished()
        {
            if (!canMove)
            {
                Destroy(gameObject);
            }
        }
        public override string GetName()
        {
            return "Spike Attack";
        }

    }
}