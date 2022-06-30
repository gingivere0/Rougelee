using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rougelee;

namespace Rougelee
{
    public class Fireball : ShotProjectile
    {

        void Awake()
        {
            damage = 10;
        }

        protected override void Hit(Enemy enemy)
        {
            base.Hit(enemy);
            SetXP();


            movespeed = 0;


            if (UpgradeTree.aoeFire)
            {
                myAnim.Play("fireballhit");
                transform.localScale = new Vector3(3,3);
                gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                Destroy(gameObject, .6f);
            }
            else
            {
                Destroy(gameObject, 0f);
            }
        }

        private void SetXP()
        {
            weaponXPBar = player.GetComponent<Player>().weaponXPBars[UpgradeTree.fireballXPBarIndex];

            UpgradeTree.fireballXP += damage;
            if (UpgradeTree.fireballXP > UpgradeTree.fireballNextLevelXP)
            {
                UpgradeTree.fireballLevel++;
                UpgradeTree.fireballNextLevelXP *= UpgradeTree.nextLevelMult;
                UpgradeTree.fireballXP = 0;
                //levelText.text = "Level " + level;
                weaponXPBar.transform.parent.GetChild(3).GetComponent<TMPro.TextMeshProUGUI>().SetText("" + UpgradeTree.fireballLevel);
            }
            weaponXPBar.SetXP(UpgradeTree.fireballXP, UpgradeTree.fireballNextLevelXP);
        }

        public override string GetName()
        {
            return "Fireball Spell";
        }

        public override Upgrade[] GetUpgrades()
        {
            upgrades = new Upgrade[5];
            PopulateUpgrades();
            return upgrades;
        }
        void PopulateUpgrades()
        {
            if (!UpgradeTree.aoeFire && UpgradeTree.fireballLevel >= 10){
                upgrades[0] = new Upgrade("Fireball explodes on hit", AOEFire);
            }
        }

        public void AOEFire()
        {
            UpgradeTree.aoeFire = true;
        }
    }
}
