using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rougelee
{
    public class Executioner : Enemy
    {

        bool bossKilled = false;
        bool facingLeft = true;
        Vector2 myScale;
        protected Vector2 portalPos;
        float attackTime;
        public bool isAttacking = false;
        public GameObject summon;
        GameObject[] summons;
        float summonTime;
        int summonCount;
        bool isShooting = false;
        SpriteRenderer sr;



        // Start is called before the first frame update
        void Start()
        {

            myID = gameObject.transform.GetInstanceID();
            playerObject = GameObject.FindGameObjectWithTag("Player");
            myAnim = GetComponent<Animator>();
            rb = GetComponent<Rigidbody2D>();
            sr = GetComponent<SpriteRenderer>();
            canMove = true;
            initMovespeed = movespeed;
            transform.localScale = transform.localScale * 1.5f;
            hp *= 10;
            summons = new GameObject[4];

        }

        private void FixedUpdate()
        {
            transform.rotation = Quaternion.identity;
            if (Vector3.Distance(playerObject.transform.position, transform.position) > 32)
            {
                Vector2 newPos = playerObject.transform.position;
                newPos.x += 20;
                transform.position = newPos;
            }

            targetPos = playerObject.transform.position;
            if (Vector3.Distance(playerObject.transform.position, transform.position) < 5 && (Time.time - attackTime > 5 || isShooting) && !isAttacking)
            {
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
            if (canMove)// && Vector3.Distance(playerObject.transform.position, transform.position) >10)
            {
                FacePlayer();
                Move();
            }

            if (!isAttacking && Time.time - summonTime > 3f && summonCount<4 && !isShooting)
            {
                Summon();
            }

            if(!isAttacking && summonCount == 4)
            {
                isShooting = true;
            }

            MovespeedCalc();

        }

        private void MovespeedCalc()
        {
            if (stunTime < 0)
            {
                canMove = true;
                stunTime = 2;
            }
            if (movespeed < initMovespeed && !isAttacking)
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
            //approach quickly, then approach slowly once in attacking distance
            if (!isAttacking && Vector3.Distance(playerObject.transform.position, transform.position) > 5)
            {
                movespeed = initMovespeed;
                rb.velocity = (targetPos - (Vector2)transform.position).normalized * new Vector2(movespeed, movespeed);
            }else if (!isAttacking)
            {
                movespeed = initMovespeed;
                rb.velocity = (targetPos - (Vector2)transform.position).normalized * new Vector2(movespeed/4, movespeed/4);
            }
        }



        protected void Attack()
        {
            myAnim.Play("executionerattack");
            isAttacking = true;

        }

        protected void FinishedAttacking()
        {
            isAttacking = false;
            if (!isShooting)
            {
                myAnim.Play("executioneridle");
            }
            attackTime = Time.time;
        }

        void Summon()
        {
            myAnim.Play("executionerskill");
        }

        protected void FinishedSummon(int part)
        {
            summonTime = Time.time;
            if (part == 0)
            {
                summons[summonCount] = Instantiate(summon, transform.position+sr.bounds.extents, Quaternion.identity);
                summonCount++;
            }
            if(part == 1)
            {
                myAnim.Play("executioneridle");
            }
        }

        void Shoot()
        {
            if (isShooting)
            {
                if (summons[summonCount - 1] != null)
                {

                    summons[summonCount - 1].GetComponent<Rigidbody2D>().velocity = (targetPos-(Vector2)summons[summonCount-1].transform.position).normalized * new Vector3(movespeed * 2, movespeed * 2);
                }
                summonCount--;
                if (summonCount <= 0)
                {
                    isShooting = false;
                }
            }
        }

        protected override void FacePlayer()
        {
            Vector3 vectorToTarget = targetPos - (Vector2)transform.position;
            float angle = (Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg);
            if (angle < 90 && angle > -90 && !facingLeft)
            {
                myScale = transform.localScale;
                myScale.x *= -1;
                transform.localScale = myScale;
                facingLeft = true;
            }
            else if ((angle > 90 || angle < -90) && facingLeft)
            {
                myScale = transform.localScale;
                myScale.x *= -1;
                transform.localScale = myScale;
                facingLeft = false;
            }
            angle = 0;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, q, Time.deltaTime * 10000f);
        }

        public override void Dead(float deathTimer = .8f, string deathAnim = "executionerdeath")
        {
            foreach(GameObject GO in summons)
            {
                if(GO != null)
                {
                    Destroy(GO);
                }
            }
            base.Dead(deathTimer, deathAnim);
            myAnim.Play("executionerdeath");
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