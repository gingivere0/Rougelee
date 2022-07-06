using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rougelee
{
    public class Enemy : MonoBehaviour
    {

        public float hp = 10;
        public float movespeed;
        protected float initMovespeed;
        public Animator myAnim;

        protected GameObject playerObject;
        protected Rigidbody2D rb;

        protected Vector2 targetPos;

        protected bool canMove;

        public int myID;

        protected int lastProjectileID;
        protected int xpdrop = 10;

        protected bool invuln = false;
        protected float invulnStartTime;
        //string lasthit = "";
        protected string lasthit;
        protected bool killCounted = false;
        protected float stunTime = 2;

        protected bool isFrozen = false;


        // Start is called before the first frame update
        void Start()
        {

            myID = gameObject.transform.GetInstanceID();
            playerObject = GameObject.FindGameObjectWithTag("Player");
            myAnim = GetComponent<Animator>();
            rb = GetComponent<Rigidbody2D>();
            canMove = true;
            initMovespeed = movespeed;

            // TODO: MAKE THIS GOOD
            hp = hp + hp * 2*UIManager.minutes;
        }

        // Update is called once per frame
        void Update()
        {
            //if enemy gets too far away, kill it and spawn another
            

        }

        private void FixedUpdate()
        {
            bool tooFar = Vector3.Distance(playerObject.transform.position, transform.position) > 32;
            if (tooFar && gameObject.tag != "Boss")
            {
                Destroy(gameObject);
            }else if (tooFar && gameObject.tag == "Boss")
            {
                Vector2 newPos = playerObject.transform.position;
                newPos.x += 20;
                transform.position = newPos;
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
            if(movespeed < initMovespeed)
            {
                movespeed += Time.deltaTime*5;
            }else if (movespeed > initMovespeed)
            {
                movespeed = initMovespeed;
            }
            stunTime -= Time.deltaTime;
        }

        protected virtual void Move()
        {

            rb.velocity = (targetPos - (Vector2)transform.position).normalized * new Vector2(movespeed, movespeed);
        }

        //gets the vector towards the target, figures out the angle, rotates towards that angle at a speed of 10000f;
        protected virtual void FacePlayer()
        {
            Vector3 vectorToTarget = targetPos - (Vector2)transform.position;
            float angle = (Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg) - 90;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, q, Time.deltaTime * 10000f);
        }



        public bool Hit(ShotProjectile projectile)
        {
            if ( !invuln || (lasthit != null && !(projectile.GetType().Name.Equals(lasthit))))
            {
                if((projectile.GetType().Name == "Sword" && UpgradeTree.iceSword ) || projectile.GetType().Name == "Ice")
                {
                    if (isFrozen)
                    {
                        hp -= projectile.damage;
                        canMove = false;
                    }
                    else
                    {
                        isFrozen = true;
                    }
                }
                hp -= projectile.damage;

                DamagePopup.Create(transform.GetComponent<Renderer>().bounds.center, (int)projectile.damage);
                invulnStartTime = Time.time;
                invuln = true;

                lasthit = projectile.GetType().Name;
                //canMove = false;
                stunTime = 2;
                movespeed *= -1;
                return true;
            }
            
            return false;
        }

        public virtual void Dead(float deathTimer = .6f, string deathAnim = "splatter")
        {
            if (!killCounted)
            {
                UIManager.kills++;
                killCounted = true;
                if (UIManager.kills % 20 == 0)
                {
                    Transform chest = Instantiate(GameAssets.i.chest, transform.position, Quaternion.identity);
                    //chest.gameObject.animator
                }
                playerObject.GetComponent<Player>().xp+=xpdrop;
            }

            gameObject.layer = 9;
            rb.velocity = Vector3.zero;
            movespeed = 0;
            canMove = false;
            targetPos = transform.position;
            if (deathAnim == null)
            {
                myAnim.Play("splatter");
            }
            else
            {
                myAnim.Play(deathAnim);
            }
            //transform.Rotate(0, 0, 270);
            if (deathTimer == 0)
            {
                Destroy(gameObject, .6f);
            }
            else
            {
                Destroy(gameObject, deathTimer);
            }
        }
    }
}