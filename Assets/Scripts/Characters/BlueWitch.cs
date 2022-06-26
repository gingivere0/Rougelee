using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rougelee
{
    public class BlueWitch : Character
    {

        public override bool UseWeapon(int weapon)
        {
            if (base.UseWeapon(weapon))
            {
                myAnim.Play("bluewitchattack");
                return true;
            }
            return false;
        }

        private void FixedUpdate()
        {
            RunWalk();
            FaceCrosshair();

        }

        void RunWalk()
        {
            if (startRunning && !isRunning && !isAttacking)
            {
                isRunning = true;
                myAnim.Play("bluewitchrun");
            }
            else if (!startRunning && !isAttacking)
            {
                isRunning = false;
                myAnim.Play("bluewitch");
            }
        }

        void FaceCrosshair()
        {
            if (crosshair.transform.position.x < transform.position.x)
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