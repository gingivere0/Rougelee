using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rougelee
{
    public class Witch : Character
    {

        public override void UseWeapon(int weapon)
        {
            base.UseWeapon(weapon);
            if (weapon == 0)
            {
                myAnim.Play("witchfire");
            }
            else
            {
                myAnim.Play("witchlightning");
            }
        }
    }
}