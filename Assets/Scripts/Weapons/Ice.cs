using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rougelee
{
    public class Ice : ShotProjectile
    {
        bool animationFinished = false;
        Vector3 crosshairPosition;

        public static XPBar icebar;

        private void Awake()
        {
            movespeed = 0;
            damage = 20;
            cooldown = .5f;
            crosshairPosition = GameObject.FindGameObjectWithTag("Player").transform.GetChild(3).transform.position;
        }

        void FixedUpdate()
        {
            //if bullet gets too far away, it disappears
            if (Vector3.Distance(player.transform.position, transform.position) > 40)
            {
                Destroy(gameObject);
            }

            if (animationFinished)
            {
                Destroy(gameObject);
            }
        }

        protected override void Hit(Enemy enemy)
        {
            base.Hit(enemy);
            if(player == null)
            {
                player = GameObject.FindGameObjectWithTag("Player");
            }
            SetXP();
        }

        private void SetXP()
        {
            if (icebar == null)
            {
                icebar = player.GetComponent<Player>().weaponXPBars[UpgradeTree.iceXPBarIndex];
            }
            
            UpgradeTree.iceXP += damage;
            if (UpgradeTree.iceXP > UpgradeTree.iceNextLevelXP)
            {
                UpgradeTree.iceLevel++;
                UpgradeTree.iceNextLevelXP *= UpgradeTree.nextLevelMult;
                UpgradeTree.iceXP = 0;
                icebar.transform.parent.GetChild(3).GetComponent<TMPro.TextMeshProUGUI>().SetText("" + UpgradeTree.iceLevel);
            }
            icebar.SetXP(UpgradeTree.iceXP, UpgradeTree.iceNextLevelXP);
        }

        public override Upgrade[] GetUpgrades()
        {
            upgrades = new Upgrade[5];
            PopulateUpgrades();
            return upgrades;
        }
        void PopulateUpgrades()
        {
            upgrades[0] = new Upgrade("Lower Ice Rain Cooldown by 10%", LowerCD);
        }


        void LowerCD()
        {
            cooldown *= .9f;
        }

        //ice falls at the position of the crosshair
        public override void Shoot(Vector3 gunDirection)
        {
            transform.position = crosshairPosition;
            transform.rotation = Quaternion.identity;
        }

        public void AnimationFinished()
        {
            animationFinished = true;
        }

        public override void Reset()
        {
            base.Reset();
            animationFinished = false;
        }

        public override string GetName()
        {
            return "Ice Rain";
        }
    }
}