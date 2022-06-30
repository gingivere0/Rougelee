using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rougelee
{
    public class Spike : ShotProjectile
    {

        public static bool canMove = false;


        private void Awake()
        {
            movespeed = 0;
            damage = 20;
        }


        protected override void Hit(Enemy enemy)
        {
            base.Hit(enemy);
            SetXP();
        }

        private void SetXP()
        {
            if (weaponXPBar == null)
            {
                weaponXPBar = player.GetComponent<Player>().weaponXPBars[UpgradeTree.spikeXPBarIndex];
            }
            UpgradeTree.spikeXP += damage;
            if (UpgradeTree.spikeXP > UpgradeTree.spikeNextLevelXP)
            {
                UpgradeTree.spikeLevel++;
                UpgradeTree.spikeNextLevelXP *= UpgradeTree.nextLevelMult;
                UpgradeTree.spikeXP = 0;
                //levelText.text = "Level " + level;
                weaponXPBar.transform.parent.GetChild(3).GetComponent<TMPro.TextMeshProUGUI>().SetText("" + UpgradeTree.spikeLevel);
            }
            weaponXPBar.SetXP(UpgradeTree.spikeXP, UpgradeTree.spikeNextLevelXP);
        }

        public override Upgrade[] GetUpgrades()
        {
            upgrades = new Upgrade[5];
            PopulateUpgrades();
            return upgrades;
        }
        void PopulateUpgrades()
        {
            upgrades[0] = new Upgrade("Lower Spike Cooldown by 10%", LowerCD);
        }


        void LowerCD()
        {
            cooldown *= .9f;
        }
        


        void FixedUpdate()
        {
            //if bullet gets too far away, it disappears
            if (Vector3.Distance(player.transform.position, transform.position) > 40)
            {
                Destroy(gameObject);
            }
        }

        //since the spike doesn't move, override the shoot method which gives the spike velocity
        //maybe upgrades make the spike projectile move, so call the base method if upgrade attained
        public override void Shoot(Vector3 gunDirection)
        {
            if (canMove)
            {
                base.Shoot(gunDirection);
            }
            else
            {
                player = GameObject.FindGameObjectWithTag("Player");
                myAnim = GetComponent<Animator>();
            }

            if (gunDirection.x < 0)
            {
                //GetComponent<SpriteRenderer>().flipY = true;
                Vector2 flipx = new Vector2(gameObject.transform.localScale.x, gameObject.transform.localScale.y*-1);
                gameObject.transform.localScale = flipx;
            }
        }

        public void AnimationFinished()
        {
            if (!canMove)
            {
                Destroy(gameObject);
            }
        }
        public override string GetName()
        {
            return "Spike Attack";
        }

    }
}