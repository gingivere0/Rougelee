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




        private void Awake()
        {
        }

        // Start is called before the first frame update
        void Start()
        {
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
            hp -= projectile.getDamage();
            if (hp <= 0)
            {
                Dead();
            }
            return hp;
        }

        public void Dead()
        {
            Destroy(transform.gameObject, 0f);
        }

    }
}