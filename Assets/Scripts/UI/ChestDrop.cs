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
            }
            rewardIndices = new int[2];
            GenerateIndices();
        }

        private void PopulateWeaponList()
        {
            int firstEmpty = -1;
            for (int i = 0; i < sps.Length; i++)
            {
                GameObject newWeapon = null;
                if (sps[i] == null)
                {
                    if (firstEmpty == -1)
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

        private GameObject GenerateWeapon()
        {
            GameObject newWeapon = null;
            while (newWeapon == null)
            {
                //get random GO projectile from list
                int newWeaponInd = Random.Range(0, player.character.projectileArray.Length);
                //check every weapon the player has equipped
                foreach(Gun weapon in player.character.weapons){

                    //keep new GO projectile
                    newWeapon = player.character.projectileArray[newWeaponInd];
                    

                    //if the player has the new weapon equipped, restart the while loop and try again
                    if (weapon != null && newWeapon.GetComponent<ShotProjectile>().GetType() == weapon.projectile.GetComponent<ShotProjectile>().GetType())
                    {
                        newWeapon = null;
                        break;
                    }
                    
                }
            }
            return newWeapon;
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