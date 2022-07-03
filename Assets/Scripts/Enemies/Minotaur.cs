using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rougelee
{
    public class Minotaur : Enemy
    {
        bool bossKilled = false;
        bool facingLeft = true;
        Vector2 myScale;
        protected Vector2 portalPos;
        protected override void FacePlayer()
        {
            Vector3 vectorToTarget = targetPos - (Vector2)transform.position;
            float angle = (Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg);
            if (angle < 90 && angle > -90 && facingLeft)
            {
                myScale = transform.localScale;
                myScale.x *= -1;
                transform.localScale = myScale;
                facingLeft = false;
            }
            else if ((angle > 90 || angle < -90) && !facingLeft)
            {
                myScale = transform.localScale;
                myScale.x *= -1;
                transform.localScale = myScale;
                facingLeft = true;
            }
            angle = 0;
            if (facingLeft)
            {
                //angle += 179;
            }
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, q, Time.deltaTime * 10000f);
        }

        public override void Dead(float deathTimer = .6f, string deathAnim = "splatter")
        {
            base.Dead();
            if (!bossKilled)
            {
                Transform chest = Instantiate(GameAssets.i.chest, transform.position, Quaternion.identity);
                portalPos = (targetPos + (new Vector2(0, 3)));
                Transform portal = Instantiate(GameAssets.i.portal, portalPos, Quaternion.identity);
                bossKilled = true;
            }
        }
    }
}