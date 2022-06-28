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
            Debug.Log("aoe unlocked");
            UpgradeTree.aoeFire = true;
        }
    }
}
