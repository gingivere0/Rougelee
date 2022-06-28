using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rougelee
{
    public class Tornado : ShotProjectile
    {
        bool startCircle = false;
        Vector3 circlePos;
        float timeAlive = 7;
        bool isMainTornado = true;
        bool spawnBabies = true;


        private void Awake()
        {
            damage = 3;
            upgrades = new Upgrade[5];
            PopulateUpgrades();
        }

        public override Upgrade[] GetUpgrades()
        {
            upgrades = new Upgrade[5];
            PopulateUpgrades();
            return upgrades;
        }
        void PopulateUpgrades()
        {
            if (!UpgradeTree.bigTornado)
            {
                upgrades[0] = new Upgrade("Increase the size and power of the tornado", BigTornado);
            }
            else if (!UpgradeTree.multiTornado)
            {
                upgrades[0] = new Upgrade("Tornado Spawns smaller Tornadoes", MultiTornado);
            }
        }

        void BigTornado()
        {
            damage *= 3;
            UpgradeTree.bigTornado = true;
        }

        void MultiTornado()
        {
            UpgradeTree.multiTornado = true;
        }

        void FixedUpdate()
        {
            //if bullet gets too far away, it disappears
            if (Vector3.Distance(player.transform.position, transform.position) > 40)
            {
                Destroy(gameObject);
            }

            /*if (Vector3.Distance(player.transform.position, transform.position) > 5 && !startCircle)
            {
                startCircle = true;
                circlePos = transform.position;
            }*/
            if (startCircle && (!isMainTornado||!UpgradeTree.multiTornado))
            {
                Circle(circlePos);
            }
            if (UpgradeTree.bigTornado && isMainTornado && startCircle)
            {
                transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(4,4), .05f);
            }

            timeAlive -= Time.deltaTime;
            if(timeAlive < 0f)
            {
                Destroy(gameObject);
            }
            transform.rotation = Quaternion.identity;
        }

        new void OnTriggerEnter2D(Collider2D col)
        {
            base.OnTriggerEnter2D(col);
            if (!startCircle)
            {
                //defines circling position as 2 units towards the player from the position of the tornado
                circlePos = player.transform.position - transform.position;
                circlePos.Normalize();
                circlePos = transform.position + new Vector3(circlePos.x * CircleRadius, circlePos.y * CircleRadius);
                angle = Mathf.Atan2(transform.position.y - circlePos.y, transform.position.x - circlePos.x);// * Mathf.Rad2Deg;
                bool flip = true;
                if (!startCircle && UpgradeTree.multiTornado && isMainTornado && flip)
                {
                    GameObject northObj = Instantiate(gameObject, circlePos + new Vector3(0f, 2f), Quaternion.identity);
                    Tornado northBaby = northObj.GetComponent<Tornado>();
                    northBaby.Reset();
                    northBaby.isMainTornado = false;
                    northBaby.circlePos = circlePos;
                    northBaby.startCircle = true;
                    northBaby.angle = Mathf.PI*3/2;
                    northBaby.CircleRadius = 2;

                    GameObject southObj = Instantiate(gameObject, circlePos + new Vector3(0f, -2f), Quaternion.identity);
                    Tornado southBaby = southObj.GetComponent<Tornado>();
                    southBaby.Reset();
                    southBaby.isMainTornado = false;
                    southBaby.circlePos = circlePos;
                    southBaby.startCircle = true;
                    southBaby.angle = Mathf.PI / 2;
                    southBaby.CircleRadius = 2;

                    GameObject eastObj = Instantiate(gameObject, circlePos + new Vector3(2f, 0f), Quaternion.identity);
                    Tornado eastBaby = eastObj.GetComponent<Tornado>();
                    eastBaby.Reset();
                    eastBaby.isMainTornado = false;
                    eastBaby.circlePos = circlePos;
                    eastBaby.startCircle = true;
                    eastBaby.angle = Mathf.PI;
                    eastBaby.CircleRadius = 2;

                    GameObject westObj = Instantiate(gameObject, circlePos + new Vector3(-2f, 0f), Quaternion.identity);
                    Tornado westBaby = westObj.GetComponent<Tornado>();
                    westBaby.Reset();
                    westBaby.isMainTornado = false;
                    westBaby.circlePos = circlePos;
                    westBaby.startCircle = true;
                    westBaby.angle = Mathf.PI*2;
                    westBaby.CircleRadius = 2;

                    gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                }else if (!startCircle && UpgradeTree.multiTornado && isMainTornado)
                {
                    GameObject northObj = Instantiate(gameObject, transform.position + new Vector3(0f, 2f), Quaternion.identity);
                    Tornado northBaby = northObj.GetComponent<Tornado>();
                    northBaby.Reset();
                    northBaby.isMainTornado = false;
                    northBaby.circlePos = transform.position;
                    northBaby.startCircle = true;
                    northBaby.angle = Mathf.PI * 3 / 2;
                    northBaby.CircleRadius = 2;

                    GameObject southObj = Instantiate(gameObject, transform.position + new Vector3(0f, -2f), Quaternion.identity);
                    Tornado southBaby = southObj.GetComponent<Tornado>();
                    southBaby.Reset();
                    southBaby.isMainTornado = false;
                    southBaby.circlePos = transform.position;
                    southBaby.startCircle = true;
                    southBaby.angle = Mathf.PI / 2;
                    southBaby.CircleRadius = 2;

                    GameObject eastObj = Instantiate(gameObject, transform.position + new Vector3(2f, 0f), Quaternion.identity);
                    Tornado eastBaby = eastObj.GetComponent<Tornado>();
                    eastBaby.Reset();
                    eastBaby.isMainTornado = false;
                    eastBaby.circlePos = transform.position;
                    eastBaby.startCircle = true;
                    eastBaby.angle = Mathf.PI;
                    eastBaby.CircleRadius = 2;

                    GameObject westObj = Instantiate(gameObject, transform.position + new Vector3(-2f, 0f), Quaternion.identity);
                    Tornado westBaby = westObj.GetComponent<Tornado>();
                    westBaby.Reset();
                    westBaby.isMainTornado = false;
                    westBaby.circlePos = transform.position;
                    westBaby.startCircle = true;
                    westBaby.angle = Mathf.PI * 2;
                    westBaby.CircleRadius = 2;

                    gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                }
            }
            startCircle = true;
        }


        float CircleRadius = 1;

        private Vector3 positionOffset;
        private float angle;

        private void Circle(Vector3 circlePos)
        {
            positionOffset.Set(Mathf.Cos(angle) * CircleRadius, Mathf.Sin(angle) * CircleRadius,1 );
            transform.position = circlePos + positionOffset;
            angle += Time.deltaTime * movespeed*.8f;
        }

        public override string GetName()
        {
            return "Tornado Spell";
        }

        private void Reset()
        {
            angle = 0;
            positionOffset = new Vector3();
            startCircle = false;
            circlePos = new Vector3();
            timeAlive = 7;
            isMainTornado = true;
            spawnBabies = true;
        }
    }
}