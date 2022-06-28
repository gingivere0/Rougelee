using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rougelee
{
    public class Sword : ShotProjectile
    {
        public int numSwings = 1;

        private void Awake()
        {
            movespeed = 5;
            damage = 20;
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
            if (!UpgradeTree.persistentSword)
            {
                upgrades[0] = new Upgrade("Add persistence to your Sword Attack", PersistentSword);
            }
            else if (!UpgradeTree.fireSword && UpgradeTree.aoeFire)
            {
                upgrades[0] = new Upgrade("Set your sword on fire!", FireBlade);
            }else if (UpgradeTree.fireSword && !UpgradeTree.movingFireSword)
            {
                upgrades[0] = new Upgrade("Shoot a firey sword!", ShootBlade);
            }
        }

        void PersistentSword()
        {
            numSwings = 6;
            UpgradeTree.persistentSword = true;
        }

        void FireBlade()
        {
            damage = 40;
            UpgradeTree.fireSword = true;
        }

        void ShootBlade()
        {
            UpgradeTree.movingFireSword = true;
        }

        void FixedUpdate()
        {
            //if bullet gets too far away, it disappears
            if (Vector3.Distance(player.transform.position, transform.position) > 40)
            {
                Destroy(gameObject);
            }
        }

        //since the sword doesn't move, override the shoot method which gives the sword velocity
        //upgrades make the sword projectile move, so call the base method if upgrade attained
        public override void Shoot(Vector3 gunDirection)
        {
            if (UpgradeTree.movingFireSword)
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
            if (!UpgradeTree.movingFireSword)
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
            if (UpgradeTree.fireSword)
            {
                return "firesword";
            }
            return "swordswing";
        }

        public override void Reset()
        {
            base.Reset();
            numSwings = 1;
        }
    }
}