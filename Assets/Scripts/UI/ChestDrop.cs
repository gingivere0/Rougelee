using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rougelee
{
    public class ChestDrop
    {
        int[] rewardIndices;
        int startInd = 0;
        Player player;

        List<Upgrade> upgrades = new List<Upgrade>();

        ShotProjectile[] sps;

        public ChestDrop(int dropType)
        {


            player = (Player)GameObject.Find("player").GetComponent(typeof(Player));
            sps = new ShotProjectile[player.character.weapons.Length];
            for (int i = 0; i < sps.Length; i++)
            {
                if (player.character.weapons[i] != null)
                {
                    sps[i] = (ShotProjectile)player.character.weapons[i].GetProjectile().GetComponent(typeof(ShotProjectile));
                }
            }
            if (dropType == 0)
            {
                PopulateUpgradeList();
            }
            else if(dropType == 1)
            {
                PopulateWeaponList();
            }else if (dropType == 2)
            {
                upgrades.Add(new Upgrade("Automatically use all equipped weapons!", () =>
                {
                    UpgradeTree.autoShoot = true;
                }));
                upgrades.Add(new Upgrade("Discard", () =>{}));
            }
            rewardIndices = new int[2];
            GenerateIndices();
            if(dropType == 2)
            {
                rewardIndices[0] = 0;
                rewardIndices[1] = 1;
            }
        }

        private void PopulateWeaponList()
        {
            int firstEmpty = -1;
            //find the first empty weapon slot on the character
            for (int i = 0; i < sps.Length; i++)
            {
                if (sps[i] == null)
                {
                    firstEmpty = i;
                    break;
                }
            }

            //cycle through all possible weapons and all equipped weapons, if possible weapon is in any equipped slot, don't add, otherwise add to first empty weapon slot
            foreach (GameObject projectileGO in player.character.projectileArray)
            {
                bool addWeapon = true;
                foreach (ShotProjectile playerWeapon in sps)
                {
                    if (playerWeapon != null && playerWeapon.GetName() != null && projectileGO.GetComponent<ShotProjectile>().GetName() == playerWeapon.GetName())
                    {
                        addWeapon = false;
                    }
                }
                if (addWeapon)
                {
                    upgrades.Add(new Upgrade("Get " + projectileGO.GetComponent<ShotProjectile>().GetName() + "!", () =>
                    {
                        player.character.weapons[firstEmpty] = new Gun(projectileGO);
                        player.character.Setup();
                        player.character.Setup();
                    }));
                }
            }
        }

        private void PopulateUpgradeList()
        {
            //upgrades.Add(new Upgrade("Increase Projectile Count by 2!", ()=> player.mods.bulletMultiplier += 2));
            upgrades.Add(new Upgrade("Increase Weapon Damage by 10%!", () => player.mods.damageMod *= 1.1f));
            if (player.hp < 5)
            {
                upgrades.Add(new Upgrade("Heal One Health Point", () => player.SetHP(player.hp + 1)));
            }
            foreach(Upgrade upgrade in sps[0].GetUpgrades())
            {
                if (upgrade != null)
                {
                    upgrades.Add(upgrade);
                }
            }
            //int firstEmpty = -1;
            for (int i = 0; i < sps.Length; i++)
            {
               /* GameObject newWeapon = null;
                if (sps[i] == null)
                {
                    if(firstEmpty == -1)
                    {
                        firstEmpty = i;
                    }
                    newWeapon = GenerateWeapon();
                    upgrades.Add(new Upgrade("Get " + newWeapon.gameObject.GetComponent<ShotProjectile>().GetName() + "!", () =>
                    {
                        player.character.weapons[firstEmpty] = new Gun(newWeapon);
                        player.character.Setup();
                        player.character.Setup();
                    }));
                }*/
                if (sps[i] != null) 
                {
                    foreach (Upgrade upgrade in sps[i].GetUpgrades())
                    {
                        if (upgrade != null)
                        {
                            upgrades.Add(upgrade);
                        }
                    }
                }
            }

        }

        private void GenerateIndices()
        {
            rewardIndices[0] = Random.Range(0, upgrades.Count);
            rewardIndices[1] = Random.Range(0, upgrades.Count);
            int loops = 5000;
            while(rewardIndices[0] == rewardIndices[1] && loops > 0)
            {
                loops--;
                rewardIndices[1] = Random.Range(0, upgrades.Count);
            }
        }

        public virtual void ReceiveTreasure(int rewardInd)
        {
            upgrades[rewardIndices[rewardInd]].PerformUpgrade();

        }


        public virtual string GetText(int rewardInd)
        {
            return upgrades[rewardIndices[rewardInd]].GetText();
        }
    }
}