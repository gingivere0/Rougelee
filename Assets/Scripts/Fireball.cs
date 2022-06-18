using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rougelee;

namespace Rougelee
{
    public class Fireball : ShotProjectile
    {

        protected override void Hit()
        {
            gameObject.layer = 9;
            movespeed = 0;
            myAnim.Play("fireballhit");
            Destroy(gameObject,.6f);
        }
    }
}
