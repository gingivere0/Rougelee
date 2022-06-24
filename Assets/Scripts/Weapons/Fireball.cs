using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rougelee;

namespace Rougelee
{
    public class Fireball : ShotProjectile
    {

        void Awake()
        {
            damage = 10;
        }

        protected override void Hit(Enemy enemy)
        {
            gameObject.layer = 9;
            movespeed = 0;
            //myAnim.Play("fireballhit");
            //Destroy(gameObject,.6f);
            Destroy(gameObject, 0f);
        }
    }
}
