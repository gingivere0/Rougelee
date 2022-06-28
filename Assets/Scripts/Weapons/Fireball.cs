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
            gameObject.layer = 9;
            movespeed = 0;
            //myAnim.Play("fireballhit");
            //Destroy(gameObject,.6f);
            Destroy(gameObject, 0f);
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
            if (!UpgradeTree.aoeFire){
                upgrades[0] = new Upgrade("Fireball explodes on hit", AOEFire);
            }
        }

        public void AOEFire()
        {
            UpgradeTree.aoeFire = true;
        }
    }
}
