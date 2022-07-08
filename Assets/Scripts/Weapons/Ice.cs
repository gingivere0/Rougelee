using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rougelee
{
    public class Ice : ShotProjectile
    {
        int numSwings = 0;
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

            if(UpgradeTree.iceLevel >= 5 && !UpgradeTree.manyIce)
            {
                upgrades[1] = new Upgrade("Increase Ice Rain Amount", ManyIce);
            }
        }

        void ManyIce()
        {
            cooldown = 1f;
            damage *= 2f;
            transform.localScale = new Vector3(2, 2);
            UpgradeTree.manyIce = true;
        }


        void LowerCD()
        {
            cooldown *= .9f;
        }

        //ice falls at the position of the crosshair
        public override void Shoot(Vector3 gunDirection)
        {
            base.Shoot(gunDirection);

            //gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            transform.position = crosshairPosition;
            transform.rotation = Quaternion.identity;
            
        }

        public override string GetAnimName()
        {
            if (UpgradeTree.manyIce)
            {
                return "manyIce";
            }
            return "ice";
        }

        public override void Reset()
        {
            base.Reset();
        }

        public override string GetName()
        {
            return "Ice Rain";
        }

        public void AnimationFinished()
        {
            if (UpgradeTree.manyIce)
            {
                numSwings--;
                if (numSwings <= 0)
                {
                    Destroy(gameObject);
                }
            }
            else
            {
                Destroy(gameObject);
            }

        }
    }
}