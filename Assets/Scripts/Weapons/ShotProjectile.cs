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
        protected GameObject player;

        protected float movespeed = 4;
        public int bulletPen = 1;
        Vector2 direction;

        protected int lastEnemyID;

        public float damage = 0;

        protected Upgrade[] upgrades;


        public virtual Upgrade[] GetUpgrades()
        {
            return upgrades;
        }


        public virtual void Reset()
        {
            damage = 0;
        }

        // Start is called before the first frame update
        void Start()
        {
            crosshair = GameObject.FindGameObjectWithTag("Crosshair");
            player = GameObject.FindGameObjectWithTag("Player");
            myAnim = GetComponent<Animator>();

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

        protected void OnTriggerEnter2D(Collider2D col)
        {
            if (col != null && col.gameObject != null && (col.gameObject.tag == "Enemy" || col.gameObject.tag == "Boss"))
            {
                //ShotProjectiles do a random amount +/-10% of base damage
                System.Random rand = new System.Random();
                if (rand.Next(0, 2) > 0)
                {
                    damage += damage * ((float)rand.Next(0, 20) / 100f);
                }
                else
                {
                    damage -= damage * (float)rand.Next(0, 20) / 100f;
                }


                Enemy enemy = (Enemy)col.gameObject.GetComponent(typeof(Enemy));
                if (enemy.hp > 0)
                {
                    bool isHit = enemy.Hit((ShotProjectile)gameObject.GetComponent(typeof(ShotProjectile)));

                    bulletPen--;
                    if (isHit)
                    {
                        Hit(enemy);
                    }
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

        public virtual void Shoot(Vector3 gunDirection)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            myAnim = GetComponent<Animator>();
            gunDirection.Normalize();
            gameObject.GetComponent<Rigidbody2D>().velocity = gunDirection * new Vector2(movespeed, movespeed);

            if (GetAnimName() != null)
            {
                myAnim.Play(GetAnimName());
            }
        }

        public virtual string GetName()
        {
            return "ShotProjectile";
        }

        public virtual void Upgrade()
        {

        }

        public virtual string GetAnimName()
        {
            return null;
        }
    }
}
