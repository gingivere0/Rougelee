using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rougelee
{
    public class Sword : ShotProjectile
    {
         int numSwings = 1;

        void Awake()
        {
            movespeed = 5;
            damage = 100;
            cooldown = 1;
            if (UpgradeTree.iceSword)
            {
                damage *= 2;
            }
            if (UpgradeTree.persistentSword)
            {
                numSwings = 6;
            }
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
            upgrades[0] = new Upgrade("Lower Sword Cooldown by 10%", LowerCD);

            //if you don't have a persistent sword and your sword is level 5
            if (!UpgradeTree.persistentSword && UpgradeTree.swordLevel >= 5)
            {
                upgrades[1] = new Upgrade("Add persistence to your Sword Attack", PersistentSword);
            }
            //if you have the ice upgrade, don't have ice sword, and your sword is level 15
            if (!UpgradeTree.iceSword && UpgradeTree.swordLevel >= 10 && UpgradeTree.manyIce)
            {
                upgrades[1] = new Upgrade("Freeze your sword, causing crits!", IceBlade);
            }

            //if you have the fire upgrade, don't have the moving sword, and your sword is level 25
            if (UpgradeTree.aoeFire && !UpgradeTree.movingSword && UpgradeTree.swordLevel >= 25)
            {
                upgrades[2] = new Upgrade("Shoot your sword forward!", ShootBlade);
            }

            if(!UpgradeTree.spinSword && UpgradeTree.multiTornado && UpgradeTree.swordLevel >= 15)
            {
                upgrades[3] = new Upgrade("Slash in a circle around you", SpinSword);
            }

        }

        void SpinSword()
        {
            UpgradeTree.spinSword = true;
        }

        void PersistentSword()
        {
            numSwings = 6;
            UpgradeTree.persistentSword = true;
        }

        void IceBlade()
        {
            damage *= 2;
            UpgradeTree.iceSword = true;
        }

        void ShootBlade()
        {
            UpgradeTree.movingSword = true;
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

        protected override void Hit(Enemy enemy)
        {
            base.Hit(enemy);
            SetXP();
        }

        private void SetXP()
        {
            if (weaponXPBar == null)
            {
                weaponXPBar = player.GetComponent<Player>().weaponXPBars[UpgradeTree.swordXPBarIndex];
            }
            UpgradeTree.swordXP += damage;
            if (UpgradeTree.swordXP > UpgradeTree.swordNextLevelXP)
            {
                UpgradeTree.swordLevel++;
                UpgradeTree.swordNextLevelXP *= UpgradeTree.nextLevelMult;
                UpgradeTree.swordXP = 0;
                //levelText.text = "Level " + level;
                damage *= 1.02f;
                weaponXPBar.transform.parent.GetChild(3).GetComponent<TMPro.TextMeshProUGUI>().SetText("" + UpgradeTree.swordLevel);
            }
            weaponXPBar.SetXP(UpgradeTree.swordXP, UpgradeTree.swordNextLevelXP);
        }

        //since the sword doesn't move, override the shoot method which gives the sword velocity
        //upgrades make the sword projectile move, so call the base method if upgrade attained
        public override void Shoot(Vector3 gunDirection)
        {
            if (UpgradeTree.movingSword)
            {
                base.Shoot(gunDirection);
            }
            else
            {
                player = GameObject.FindGameObjectWithTag("Player");
                myAnim = GetComponent<Animator>();

                if (GetAnimName() != null)
                {
                    myAnim.Play(GetAnimName());
                }
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
            if (!UpgradeTree.movingSword)
            {
                numSwings--;
                if (numSwings<=0)
                {
                    Destroy(gameObject);
                }
            }
            
        }

        public override string GetName()
        {
            return "Sword Attack";
        }

        public override string GetAnimName()
        {
            if (UpgradeTree.iceSword)
            {
                return "icesword";
            }
            return "swordswing";
        }

        public override void Reset()
        {
            base.Reset();
            Awake();
        }
    }
}