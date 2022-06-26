using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rougelee
{
    public class Witch : Character
    {

        public override bool UseWeapon(int weapon)
        {
            bool attacked = base.UseWeapon(weapon);
            if (weapon == 0 && attacked)
            {
                myAnim.Play("witchfire");
            }
            else if (attacked)
            {
                myAnim.Play("witchlightning");
            }
            return attacked;
        }
    }
}