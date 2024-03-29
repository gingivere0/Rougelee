using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rougelee
{
    public class Minotaur : Enemy
    {
        bool facingLeft = true;
        Vector2 myScale;
        protected Vector2 portalPos;

        void Start()
        {
            myID = gameObject.transform.GetInstanceID();
            playerObject = GameObject.FindGameObjectWithTag("Player");
            myAnim = GetComponent<Animator>();
            rb = GetComponent<Rigidbody2D>();
            canMove = true;
            hp *= 10;
            initMovespeed = movespeed;
        }
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
        }
    }
}