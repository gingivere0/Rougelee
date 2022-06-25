using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rougelee
{
    public class Enemy : MonoBehaviour
    {

        public float hp = 10;
        public float movespeed;
        public Animator myAnim;

        GameObject playerObject;

        protected Vector2 targetPos;

        bool canMove;

        public int myID;

        protected int lastProjectileID;
        protected int xpdrop = 10;

        bool invuln = false;
        float invulnStartTime;
        //string lasthit = "";
        string lasthit;
        bool killCounted = false;
        float stunTime = 2;


        // Start is called before the first frame update
        void Start()
        {

            myID = gameObject.transform.GetInstanceID();
            playerObject = GameObject.FindGameObjectWithTag("Player");
            myAnim = GetComponent<Animator>();
            canMove = true;

        }

        // Update is called once per frame
        void Update()
        {
            //if enemy gets too far away, kill it and spawn another
            

        }

        private void FixedUpdate()
        {
            /*xInput = Input.GetAxis("Horizontal");
            yInput = Input.GetAxis("Vertical");*/
            if (Vector3.Distance(playerObject.transform.position, transform.position) > 32)
            {
                Destroy(gameObject);
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
            }
            stunTime -= Time.deltaTime;
        }

        protected virtual void Move()
        {

            transform.position = Vector2.MoveTowards(transform.position, targetPos, movespeed);
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
                hp -= projectile.damage;

                DamagePopup.Create(transform.GetComponent<Renderer>().bounds.center, (int)projectile.damage);
                invulnStartTime = Time.time;
                invuln = true;

                lasthit = projectile.GetType().Name;
                canMove = false;
                stunTime = 2;
                return true;
            }
            return false;
        }

        public virtual void Dead()
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
            movespeed = 0;
            canMove = false;
            targetPos = transform.position;
            myAnim.Play("splatter");
            //transform.Rotate(0, 0, 270);
            Destroy(gameObject, .6f);
        }
    }
}