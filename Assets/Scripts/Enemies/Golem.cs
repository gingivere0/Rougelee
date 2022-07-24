using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rougelee
{
    public class Golem : Enemy
    {
        bool facingLeft = true;
        Vector2 myScale;
        protected Vector2 portalPos;
        float attackTime;
        public bool isAttacking = false;
        public GameObject projectile;
        GameObject[] projectiles;
        float summonTime;
        int summonCount;
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
            projectiles = new GameObject[8];

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
            if (Vector3.Distance(playerObject.transform.position, transform.position) < 8 && Time.time - attackTime > 6 && !isAttacking)
            {
                StartAttack();
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
            }
            else if (!isAttacking)
            {
                movespeed = initMovespeed;
                rb.velocity = (targetPos - (Vector2)transform.position).normalized * new Vector2(movespeed / 3, movespeed / 3);
            }
        }


        protected void StartAttack()
        {
            myAnim.Play("golemball");
            isAttacking = true;

        }

        void FinishedAnimation(int anim)
        {
            if(anim == 0)
            {
                isAttacking = false;
                attackTime = Time.time;
                myAnim.Play("golemidle");
            }
            else if(anim == 1)
            {
                Shoot();
            }
        }

        void Shoot()
        {
            //create 6 projectiles and shoot them in a circle

            for (int i = 0; i < projectiles.Length; i++)
            {
                if (i == 0)
                {
                    Destroy(projectiles[i]);
                    projectiles[i] = Instantiate(projectile, transform.position, Quaternion.identity);
                    projectiles[i].transform.Rotate(0, 0, 180);
                    projectiles[i].GetComponent<Rigidbody2D>().velocity = new Vector2(1, 0).normalized * new Vector3(movespeed * 4, movespeed * 4);
                }
                else if (i == 1)
                {
                    Destroy(projectiles[i]);
                    projectiles[i] = Instantiate(projectile, transform.position, Quaternion.identity);
                    projectiles[i].transform.Rotate(0, 0, -135);
                    projectiles[i].GetComponent<Rigidbody2D>().velocity = new Vector2(.5f, .5f).normalized * new Vector3(movespeed * 4, movespeed * 4);
                }
                else if (i == 2)
                {
                    Destroy(projectiles[i]);
                    projectiles[i] = Instantiate(projectile, transform.position, Quaternion.identity);
                    projectiles[i].transform.Rotate(0, 0, -90);
                    projectiles[i].GetComponent<Rigidbody2D>().velocity = new Vector2(0, 1).normalized * new Vector3(movespeed * 4, movespeed * 4);
                }
                else if (i == 3)
                {
                    Destroy(projectiles[i]);
                    projectiles[i] = Instantiate(projectile, transform.position, Quaternion.identity);
                    projectiles[i].transform.Rotate(0, 0, -45);
                    projectiles[i].GetComponent<Rigidbody2D>().velocity = new Vector2(-.5f, .5f).normalized * new Vector3(movespeed * 4, movespeed * 4);
                }
                else if (i == 4)
                {
                    Destroy(projectiles[i]);
                    projectiles[i] = Instantiate(projectile, transform.position, Quaternion.identity);
                    projectiles[i].GetComponent<Rigidbody2D>().velocity = new Vector2(-1, 0).normalized * new Vector3(movespeed * 4, movespeed * 4);
                }
                else if (i == 5)
                {
                    Destroy(projectiles[i]);
                    projectiles[i] = Instantiate(projectile, transform.position, Quaternion.identity);
                    projectiles[i].transform.Rotate(0, 0, 45);
                    projectiles[i].GetComponent<Rigidbody2D>().velocity = new Vector2(-.5f, -.5f).normalized * new Vector3(movespeed * 4, movespeed * 4);
                }
                else if (i == 6)
                {
                    Destroy(projectiles[i]);
                    projectiles[i] = Instantiate(projectile, transform.position, Quaternion.identity);
                    projectiles[i].transform.Rotate(0, 0, 90);
                    projectiles[i].GetComponent<Rigidbody2D>().velocity = new Vector2(0, -1).normalized * new Vector3(movespeed * 4, movespeed * 4);
                }
                else if (i == 7)
                {
                    Destroy(projectiles[i]);
                    projectiles[i] = Instantiate(projectile, transform.position, Quaternion.identity);
                    projectiles[i].transform.Rotate(0, 0, 135);
                    projectiles[i].GetComponent<Rigidbody2D>().velocity = new Vector2(.5f, -.5f).normalized * new Vector3(movespeed * 4, movespeed * 4);
                }
            }
            /*if (isShooting)
            {
                if (summons[summonCount - 1] != null)
                {

                    summons[summonCount - 1].GetComponent<Rigidbody2D>().velocity = (targetPos - (Vector2)summons[summonCount - 1].transform.position).normalized * new Vector3(movespeed * 4, movespeed * 4);
                }
                summonCount--;
                if (summonCount <= 0)
                {
                    isShooting = false;
                }
            }*/
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

        public override void Dead(float deathTimer = 1.4f, string deathAnim = "golemdeath")
        {
            
            base.Dead(deathTimer, deathAnim);
            // TODO: uncomment
            myAnim.Play("golemdeath");
        }
    }
}