using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rougelee
{
    public class Tornado : ShotProjectile
    {
        bool startCircle = false;
        Vector3 circlePos;
        float timeAlive = 5;
        float timeDead = 1;
        bool isMainTornado = true;
        float shrinkRate = .1f;

        GameObject northObj;
        GameObject southObj;
        GameObject eastObj;
        GameObject westObj;
        Tornado[] babies;


        private void Awake()
        {
            damage = 5;
            cooldown = 1;
            upgrades = new Upgrade[5];
            PopulateUpgrades();
        }

        protected override void Hit(Enemy enemy)
        {
            base.Hit(enemy);
            SetXP();
        }

        private void SetXP()
        {
            weaponXPBar = player.GetComponent<Player>().weaponXPBars[UpgradeTree.tornadoXPBarIndex];

            UpgradeTree.tornadoXP += damage;
            if (UpgradeTree.tornadoXP > UpgradeTree.tornadoNextLevelXP)
            {
                UpgradeTree.tornadoLevel++;
                UpgradeTree.tornadoNextLevelXP *= UpgradeTree.nextLevelMult;
                UpgradeTree.tornadoXP = 0;
                //levelText.text = "Level " + level;
                weaponXPBar.transform.parent.GetChild(3).GetComponent<TMPro.TextMeshProUGUI>().SetText("" + UpgradeTree.tornadoLevel);
            }
            weaponXPBar.SetXP(UpgradeTree.tornadoXP, UpgradeTree.tornadoNextLevelXP);
        }

        public override Upgrade[] GetUpgrades()
        {
            upgrades = new Upgrade[5];
            PopulateUpgrades();
            return upgrades;
        }
        void PopulateUpgrades()
        {

            upgrades[0] = new Upgrade("Lower Tornado cooldown by 10%", LowerCD);
            if (!UpgradeTree.bigTornado && UpgradeTree.tornadoLevel >= 5)
            {
                upgrades[1] = new Upgrade("Increase the size and power of the tornado", BigTornado);
            }
            else if (!UpgradeTree.multiTornado && UpgradeTree.tornadoLevel >= 15)
            {
                upgrades[1] = new Upgrade("Tornado Spawns smaller Tornadoes", MultiTornado);
            }
        }

        void LowerCD()
        {
            cooldown *= .9f;
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
            if (startCircle)//&& (!isMainTornado||!UpgradeTree.multiTornado))
            {
                Circle(circlePos);
            }

            timeAlive -= Time.deltaTime;
            if(timeAlive < 0f)
            {
                transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(0.001f, 0.001f), shrinkRate);
                CircleRadius -= shrinkRate;
                if (CircleRadius <= 0)
                {
                    CircleRadius = .001f;
                }
                if (northObj != null && babies[0] != null)
                {
                    northObj.transform.localScale = Vector3.Lerp(northObj.transform.localScale, new Vector3(0.001f, 0.001f), shrinkRate);
                    babies[0].CircleRadius-=shrinkRate;
                    if (babies[0].CircleRadius <= 0)
                    {
                        babies[0].CircleRadius = .001f;
                    }
                    
                }
                if (southObj != null && babies[1] != null)
                {
                    southObj.transform.localScale = Vector3.Lerp(southObj.transform.localScale, new Vector3(0.001f, 0.001f), shrinkRate);
                    babies[1].CircleRadius -= shrinkRate;
                    if (babies[1].CircleRadius <= 0)
                    {
                        babies[1].CircleRadius = .001f;
                    }
                }
                if (eastObj != null && babies[2] != null)
                {
                    eastObj.transform.localScale = Vector3.Lerp(eastObj.transform.localScale, new Vector3(0.001f, 0.001f), shrinkRate);
                    babies[2].CircleRadius -= shrinkRate;
                    if (babies[2].CircleRadius <= 0)
                    {
                        babies[2].CircleRadius = .001f;
                    }
                }
                if (westObj != null && babies[3] != null)
                {
                    westObj.transform.localScale = Vector3.Lerp(westObj.transform.localScale, new Vector3(0.001f, 0.001f), shrinkRate);
                    babies[3].CircleRadius -= shrinkRate;
                    if (babies[3].CircleRadius <= 0)
                    {
                        babies[3].CircleRadius = .001f;
                    }
                }
                timeDead -= Time.deltaTime;

                

                if (timeDead <= 0)
                {
                    if (gameObject != null && northObj != null & southObj != null && eastObj != null && westObj != null)
                    {
                        Destroy(gameObject); 
                        Destroy(northObj);
                        Destroy(southObj);
                        Destroy(eastObj);
                        Destroy(westObj);

                    }
                }
            }else if (UpgradeTree.bigTornado && isMainTornado && startCircle)
            {
                transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(4, 4), .05f);
            }
            transform.rotation = Quaternion.identity;
        }

        new void OnTriggerEnter2D(Collider2D col)
        {
            base.OnTriggerEnter2D(col);
            if (!startCircle)
            {
                //defines circling position as CircleRadius units towards the player from the position of the tornado
                circlePos = player.transform.position - transform.position;
                circlePos.Normalize();
                circlePos = transform.position + new Vector3(circlePos.x * CircleRadius, circlePos.y * CircleRadius);
                angle = Mathf.Atan2(transform.position.y - circlePos.y, transform.position.x - circlePos.x);// * Mathf.Rad2Deg;

                if (!startCircle && UpgradeTree.multiTornado && isMainTornado)
                {
                    CircleRadius = .5f;
                    babies = new Tornado[4];

                    northObj = Instantiate(gameObject, circlePos + new Vector3(0f, 2f), Quaternion.identity);
                    Tornado northBaby = northObj.GetComponent<Tornado>();
                    northBaby.Reset();
                    northBaby.isMainTornado = false;
                    northBaby.circlePos = circlePos;
                    northBaby.startCircle = true;
                    northBaby.angle = Mathf.PI*3/2;
                    northBaby.CircleRadius = 2;
                    northBaby.damage = damage * .4f;
                    babies[0] = northBaby;


                    southObj = Instantiate(gameObject, circlePos + new Vector3(0f, -2f), Quaternion.identity);
                    Tornado southBaby = southObj.GetComponent<Tornado>();
                    southBaby.Reset();
                    southBaby.isMainTornado = false;
                    southBaby.circlePos = circlePos;
                    southBaby.startCircle = true;
                    southBaby.angle = Mathf.PI / 2;
                    southBaby.CircleRadius = 2;
                    southBaby.damage = damage * .4f;
                    babies[1] = southBaby;

                    eastObj = Instantiate(gameObject, circlePos + new Vector3(2f, 0f), Quaternion.identity);
                    Tornado eastBaby = eastObj.GetComponent<Tornado>();
                    eastBaby.Reset();
                    eastBaby.isMainTornado = false;
                    eastBaby.circlePos = circlePos;
                    eastBaby.startCircle = true;
                    eastBaby.angle = Mathf.PI;
                    eastBaby.CircleRadius = 2;
                    eastBaby.damage = damage * .4f;
                    babies[2] = eastBaby;

                    westObj = Instantiate(gameObject, circlePos + new Vector3(-2f, 0f), Quaternion.identity);
                    Tornado westBaby = westObj.GetComponent<Tornado>();
                    westBaby.Reset();
                    westBaby.isMainTornado = false;
                    westBaby.circlePos = circlePos;
                    westBaby.startCircle = true;
                    westBaby.angle = Mathf.PI*2;
                    westBaby.CircleRadius = 2;
                    westBaby.damage = damage * .4f;
                    babies[3] = westBaby;

                    gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                }
            }
            startCircle = true;
        }


        float CircleRadius = .5f;

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

        public override void Reset()
        {
            base.Reset();
            angle = 0;
            positionOffset = new Vector3();
            startCircle = false;
            circlePos = new Vector3();
            timeAlive = 7;
            isMainTornado = true;
        }
    }
}