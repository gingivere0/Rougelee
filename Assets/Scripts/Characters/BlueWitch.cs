using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rougelee
{
    public class BlueWitch : Character
    {

        public override void UseWeapon(int weapon)
        {
            base.UseWeapon(weapon);
            myAnim.Play("bluewitchattack");
        }

        private void FixedUpdate()
        {
            RunWalk();
            LeftRight();

            //check to see if attack animation is playing
            isAttacking = !(myAnim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1);
        }

        void RunWalk()
        {
            if (startRunning && !isRunning && !isAttacking)
            {
                myAnim.Play("bluewitchrun");
            }
            else if (!startRunning && !isAttacking)
            {
                isRunning = false;
                myAnim.Play("bluewitch");
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