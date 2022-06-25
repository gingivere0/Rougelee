using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rougelee
{
    public class Worm : Enemy
    {
        bool facingRight = true;
        Vector2 myScale;
        protected override void FacePlayer()
        {
            Vector3 vectorToTarget = targetPos - (Vector2)transform.position;
            float angle = (Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg);
            //Debug.Log(angle);
            if (angle < 90 && angle > -90 && !facingRight)
            {
                myScale = transform.localScale;
                myScale.x *= -1 ;
                transform.localScale = myScale;
                facingRight = true;
            }
            else if ((angle > 90 || angle < -90) && facingRight)
            {
                myScale = transform.localScale;
                myScale.x *= -1;
                transform.localScale = myScale;
                facingRight = false;
            }
            if (!facingRight)
            {
                angle -= 180;
            }
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, q, Time.deltaTime * 10000f);
        }

        public override void Dead()
        {
            transform.localScale = new Vector3(.95f, .95f);
            base.Dead();
        }
    }
}