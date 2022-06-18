using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rougelee
{
    public class ShotProjectile : MonoBehaviour
    {
        protected Vector3 targetPos;
        public Animator myAnim;
        GameObject crosshair;
        GameObject player;

        protected int movespeed = 4;
        public int bulletPen = 1;

        protected float damage = 0;

        // Start is called before the first frame update
        void Start()
        {
            crosshair = GameObject.FindGameObjectWithTag("Crosshair");
            player = GameObject.FindGameObjectWithTag("Player");
            myAnim = GetComponent<Animator>();
            
            targetPos = crosshair.transform.position;

            transform.right = targetPos - transform.position;

            Vector3 myPos = transform.position;
            myPos.z = 0;
            transform.position = myPos;

        }

        // Update is called once per frame
        void Update()
        {
            transform.Translate(Vector3.right * Time.deltaTime * movespeed);
            //if bullet gets too far away, it disappears
            if (Vector3.Distance(player.transform.position, transform.position) > 25)
            {
                Destroy(gameObject);
            }

        }

        void OnCollisionEnter2D(Collision2D col)
        {
            if (col != null && col.gameObject != null && col.gameObject.tag == "Enemy")
            {

                _ = ((Enemy) col.gameObject.GetComponent(typeof(Enemy))).Hit((ShotProjectile)col.otherCollider.gameObject.GetComponent(typeof(ShotProjectile)));
                
                Hit();
                bulletPen--;
            }
        }

        protected virtual void Hit()
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
    }
}
