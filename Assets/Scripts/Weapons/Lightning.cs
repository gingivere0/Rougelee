using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rougelee
{
    public class Lightning : ShotProjectile
    {
        public int maxChain;

        void Awake()
        {
            maxChain = 1;
            damage = 6;
        }

        public override Upgrade[] GetUpgrades()
        {
            upgrades = new Upgrade[5];
            PopulateUpgrades();
            return upgrades;
        }
        void PopulateUpgrades()
        {
            upgrades[0] = new Upgrade("Lower Lightning Cooldown by 10%", LowerCD);
        }


        void LowerCD()
        {
            cooldown *= .9f;
        }

        public void chain()
        {
            GameObject enemy = getNearestEnemy();

            targetPos = enemy.transform.position;

            Shoot(targetPos - transform.position);

            Vector3 objectPos = transform.position;
            targetPos.x = targetPos.x - objectPos.x;
            targetPos.y = targetPos.y - objectPos.y;

            float lookAngle = Mathf.Atan2(targetPos.y, targetPos.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, lookAngle));


            Vector3 myPos = transform.position;
            myPos.z = 0;
            transform.position = myPos;
        }

        public GameObject getNearestEnemy()
        {

            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            Array.Sort(enemies, CompareDistance);

            return enemies[2];
        }

        private int CompareDistance(GameObject a, GameObject b)
        {
            return (new CaseInsensitiveComparer()).Compare(Vector2.Distance((Vector2)transform.position, (Vector2)a.transform.position), Vector2.Distance((Vector2)transform.position, (Vector2)b.transform.position));
        }

        protected override void Hit(Enemy enemy)
        {


            SetXP();
            if (lastEnemyID != enemy.myID)
            {
                if (maxChain > 0)
                {
                    maxChain--;
                    GameObject cloneObject = MonoBehaviour.Instantiate(gameObject, transform.position, Quaternion.identity);
                    Lightning clone = cloneObject.GetComponent<Lightning>();
                    clone.maxChain = maxChain;
                    clone.transform.position = transform.position;
                    clone.chain();
                    clone.lastEnemyID = enemy.myID;
                    clone.transform.parent = transform.parent;
                }
                if (bulletPen <= 0)
                {
                    gameObject.layer = 9;
                    movespeed = 0;
                    //myAnim.Play("boltstrike");
                    //transform.Rotate(0, 0, 270);
                    //Destroy(gameObject, .6f);
                    Destroy(gameObject,0f);
                }
            }


        }

        private void SetXP()
        {
            weaponXPBar = player.GetComponent<Player>().weaponXPBars[UpgradeTree.lightningXPBarIndex];

            UpgradeTree.lightningXP += damage;
            if (UpgradeTree.lightningXP > UpgradeTree.lightningNextLevelXP)
            {
                UpgradeTree.lightningLevel++;
                UpgradeTree.lightningNextLevelXP *= UpgradeTree.nextLevelMult;
                UpgradeTree.lightningXP = 0;
                //levelText.text = "Level " + level;
                weaponXPBar.transform.parent.GetChild(3).GetComponent<TMPro.TextMeshProUGUI>().SetText("" + UpgradeTree.lightningLevel);
            }
            weaponXPBar.SetXP(UpgradeTree.lightningXP, UpgradeTree.lightningNextLevelXP);
        }

        public override string GetName()
        {
            return "Lightning Spell";
        }
    }
}
