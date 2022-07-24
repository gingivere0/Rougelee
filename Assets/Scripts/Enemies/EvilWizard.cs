using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rougelee
{
    public class EvilWizard : Enemy
    {
        public GameObject greenAttack;

        bool bossKilled = false;
        bool facingLeft = true;
        Vector2 myScale;
        protected Vector2 portalPos;
        float attackTime;
        public bool startedAttack = false;
        public bool isShooting = false;
        int shotsLeft = 3;


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
            hp *= 10;

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
            if (Vector3.Distance(playerObject.transform.position, transform.position) < 15 && Time.time - attackTime > 3 && !startedAttack) 
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
            if (movespeed < initMovespeed && !startedAttack)
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
            if (!startedAttack && Vector3.Distance(playerObject.transform.position, transform.position) > 10)
            {
                movespeed = initMovespeed;
                rb.velocity = (targetPos - (Vector2)transform.position).normalized * new Vector2(movespeed, movespeed);
            }
            else if (!startedAttack && Vector3.Distance(playerObject.transform.position, transform.position) < 9.7)
            {
                rb.velocity = Vector3.zero;
                movespeed = -initMovespeed/2;
                rb.velocity = (targetPos - (Vector2)transform.position).normalized * new Vector2(movespeed, movespeed);
                myAnim.Play("wizardidle");
            }
        }

        protected void StartAttack()
        {
            startedAttack = true;
            rb.velocity = Vector3.zero;
            movespeed = 0;
            Attack();
        }

        protected void Attack()
        {
            isShooting = true;
            myAnim.Play("wizardattack");
            
            
        }

        protected void CreateProjectile()
        {
            GameObject greenAttackproj = Instantiate(greenAttack, transform.position, Quaternion.identity);
            greenAttackproj.GetComponent<GreenAttack>().Shoot((targetPos - (Vector2)greenAttackproj.transform.position));
        }

        protected void FinishedAttacking()
        {
            isShooting = false;
            shotsLeft--;
            movespeed = initMovespeed;
            if (shotsLeft <= 0)
            {
                myAnim.Play("wizardrun"); 
                attackTime = Time.time;
                startedAttack = false;
                shotsLeft = 3;
            }
            else
            {
                Attack();
            }
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
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, q, Time.deltaTime * 10000f);
        }

        public override void Dead(float deathTimer = .8f, string deathAnim = "wizarddeath")
        {
            base.Dead(deathTimer, deathAnim);
            myAnim.Play("wizarddeath");
            if (!bossKilled)
            {
                Transform obelisk = Instantiate(GameAssets.i.obelisk, transform.position, Quaternion.identity);
                portalPos = (targetPos + (new Vector2(0, 3)));
                Transform portal = Instantiate(GameAssets.i.portal, portalPos, Quaternion.identity);
                bossKilled = true;
            }
        }
    }
}