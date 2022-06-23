using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rougelee
{
    public class ShotProjectile : MonoBehaviour
    {
        public Vector3 targetPos;
        public Animator myAnim;
        GameObject crosshair;
        GameObject player;

        protected float movespeed = 4;
        public int bulletPen = 1;
        Vector2 direction;


        public int myID;
        protected int lastEnemyID;

        public float damage = 0;

        // Start is called before the first frame update
        void Start()
        {
            crosshair = GameObject.FindGameObjectWithTag("Crosshair");
            player = GameObject.FindGameObjectWithTag("Player");
            myAnim = GetComponent<Animator>();

            myID = gameObject.transform.GetInstanceID();

            /*targetPos = crosshair.transform.position;

            transform.right = targetPos - transform.position;

            Vector3 myPos = transform.position;
            myPos.z = 0;
            transform.position = myPos;
            */



            //gameObject.GetComponent<Rigidbody2D>().velocity = direction * new Vector2(movespeed, movespeed);


        }

        // Update is called once per frame
        void Update()
        {

        }

        void FixedUpdate()
        {
            //if bullet gets too far away, it disappears
            if (Vector3.Distance(player.transform.position, transform.position) > 25)
            {
                Destroy(gameObject);
            }

        }

        void OnTriggerEnter2D(Collider2D col)
        {
            if (col != null && col.gameObject != null && col.gameObject.tag == "Enemy")
            {
                Enemy enemy = (Enemy)col.gameObject.GetComponent(typeof(Enemy));
                if (enemy.hp > 0)
                {
                    _ = enemy.Hit((ShotProjectile)gameObject.GetComponent(typeof(ShotProjectile)));

                    bulletPen--;
                    Hit(enemy);
                }
            }
        }

        protected virtual void Hit(Enemy enemy)
        {
        }

        protected virtual void DestroyProjectile(GameObject obj)
        {
           //Destroy(obj, 0f);
        }

        public float getDamage()
        {
            return damage;
        }
        public void setDamage(float newDamage)
        {
            damage = newDamage;
        }

        public void Shoot(Vector3 gunDirection)
        {/*
            transform.position+=(Vector3.up*i);

            Vector3 angles = transform.eulerAngles;
            angles.z *= 90 * i;
            transform.eulerAngles = angles;
            Debug.Log("After: " + transform.rotation);
            */


            player = GameObject.FindGameObjectWithTag("Player");
            myAnim = GetComponent<Animator>();

            //float rad2deg = 180/(float)System.Math.PI;

            //direction.x = (float)System.Math.Cos(10*rad2deg*bulletNum*direction.x);
            //direction.y += bulletNum*(float)System.Math.Sin(10*rad2deg*bulletNum);
            //double angle = System.Math.Atan2(gunDirection.y, gunDirection.x)*rad2deg;

            //Vector2 bulletDirection = new Vector2((float)System.Math.Cos((angle+10*bulletNum) / rad2deg), (float)System.Math.Sin((angle+10*bulletNum) / rad2deg));


            //Debug.Log("point: " + gunDirection);
            //Debug.Log("angle: " + angle);
            //Debug.Log("Mathtest: " + bulletDirection);
            gunDirection.Normalize();



            gameObject.GetComponent<Rigidbody2D>().velocity = gunDirection * new Vector2(movespeed, movespeed);
        }
    }
}
