using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rougelee
{
    public class EvilWizard : Enemy
    {
        bool bossKilled = false;
        bool facingLeft = true;
        Vector2 myScale;
        protected Vector2 portalPos;
        float attackTime;
        bool isAttacking = false;


        // Start is called before the first frame update
        void Start()
        {

            myID = gameObject.transform.GetInstanceID();
            playerObject = GameObject.FindGameObjectWithTag("Player");
            myAnim = GetComponent<Animator>();
            rb = GetComponent<Rigidbody2D>();
            canMove = true;
            initMovespeed = movespeed;
            transform.localScale = transform.localScale * 1.5f;

        }

        private void FixedUpdate()
        {
            if (Vector3.Distance(playerObject.transform.position, transform.position) > 32)
            {
                Vector2 newPos = playerObject.transform.position;
                newPos.x += 20;
                transform.position = newPos;
            }

            if(Vector3.Distance(playerObject.transform.position, transform.position) < 10 && Time.time-attackTime>3){
                Attack();
            }
            if (hp <= 0)
            {
                Dead();
            }
            if (invuln && (Time.time - invulnStartTime) >= .5f)
            {
                invuln = false;
                gameObject.layer = 6;
            }
            targetPos = playerObject.transform.position;
            if (canMove)
            {
                FacePlayer();
                Move();
            }
            if (stunTime < 0)
            {
                canMove = true;
                stunTime = 2;
            }
            if (movespeed < initMovespeed)
            {
                movespeed += Time.deltaTime * 5;
            }
            else if (movespeed > initMovespeed)
            {
                movespeed = initMovespeed;
            }
            stunTime -= Time.deltaTime;

        }

        protected override void Move()
        {
            if (!isAttacking)
            {
                rb.velocity = (targetPos - (Vector2)transform.position).normalized * new Vector2(movespeed, movespeed);
            }
        }

        protected void Attack()
        {
            isAttacking = true;
            attackTime = Time.time;
            rb.velocity = Vector3.zero;
            movespeed = 0;
            myAnim.Play("wizardattack");
        }

        protected void FinishedAttacking()
        {
            isAttacking = false;
            movespeed = initMovespeed;
            myAnim.Play("wizardrun");
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

        public override void Dead()
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