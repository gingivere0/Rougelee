using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rougelee
{
    public class Knight : Character
    {

        public override bool UseWeapon(int weapon)
        {
            if (base.UseWeapon(weapon))
            {
                myAnim.Play("knightattack");
                return true;
            }
            return false;
        }

        private void FixedUpdate()
        {
            RunWalk();
            LeftRight();
        }

        void RunWalk()
        {
            if (startRunning && !isRunning && !isAttacking)
            {
                isRunning = true;
                myAnim.Play("knightrun");
            }
            else if (!startRunning && !isAttacking)
            {
                isRunning = false;
                myAnim.Play("knightidle");
            }
        }

        void LeftRight()
        {
            if (facingLeft)
            {
                sp.flipX = true;
            }
            else
            {
                sp.flipX = false;
            }
        }
    }
}