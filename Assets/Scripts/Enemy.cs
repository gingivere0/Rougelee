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


        GameObject player;

        Vector2 targetPos;

        bool canMove;

        public int myID;

        protected int lastProjectileID;

        bool invuln = false;
        float invulnStartTime;
        string lasthit = "";


        // Start is called before the first frame update
        void Start()
        {

            myID = gameObject.transform.GetInstanceID();
            player = GameObject.FindGameObjectWithTag("Player");
            myAnim = GetComponent<Animator>();
            canMove = true;

        }

        // Update is called once per frame
        void Update()
        {
            //if enemy gets too far away, kill it and spawn another
            if (Vector3.Distance(player.transform.position, transform.position) > 15)
            {
                Destroy(gameObject);
            }
            if (hp <= 0)
            {
                Dead();
            }
            if (invuln && (Time.time-invulnStartTime)>=.5f)
            {
                invuln = false;
                gameObject.layer = 6;
            }

        }

        private void FixedUpdate()
        {
            /*xInput = Input.GetAxis("Horizontal");
            yInput = Input.GetAxis("Vertical");*/
            targetPos = player.transform.position;
            if (canMove)
            {
                //gets the vector towards the target, figures out the angle, rotates towards that angle at a speed of 50f;
                Vector3 vectorToTarget = targetPos - (Vector2)transform.position;
                float angle = (Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg) - 90;
                Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, q, Time.deltaTime * 10000f);

                transform.position = Vector2.MoveTowards(transform.position, targetPos, movespeed);
            }
        }



        public float Hit(ShotProjectile projectile)
        {
            if (!invuln)
            {
                hp -= projectile.damage;
                invulnStartTime = Time.time;
                invuln = true;
                gameObject.layer = 9;
                if(projectile is Lightning)
                {
                    lasthit = "boltstrike";
                }
                else
                {
                    lasthit = "fireballhit";
                }
                
            }
            return hp;
        }

        public void Dead()
        {
            gameObject.layer = 9;
            movespeed = 0;
            if (lasthit.Equals("boltstrike"))
            {
                transform.localScale = new Vector3(.1f, .1f, .1f);
            }
            myAnim.Play(lasthit);
            //transform.Rotate(0, 0, 270);
            Destroy(gameObject, .5f);
        }
    }
}