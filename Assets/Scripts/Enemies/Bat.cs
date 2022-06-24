using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rougelee
{
    public class Bat : Enemy
    {

        protected override void FacePlayer()
        {
            Vector3 vectorToTarget = targetPos - (Vector2)transform.position;
            float angle = (Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg);
            //Debug.Log(angle);
            if(angle<90 && angle > -90 && myAnim.GetCurrentAnimatorStateInfo(0).IsName("batleft"))
            {
                myAnim.Play("batright");
            }
            else if((angle >90 || angle<-90)&& myAnim.GetCurrentAnimatorStateInfo(0).IsName("batright"))
            {
                myAnim.Play("batleft");
            }
            if (myAnim.GetCurrentAnimatorStateInfo(0).IsName("batleft"))
            {
                angle += 180;
            }
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, q, Time.deltaTime * 10000f);
        }

    }
}