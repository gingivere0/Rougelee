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
        GameObject newWeapon = null;

        List<Upgrade> upgrades = new List<Upgrade>();

        ShotProjectile[] sps;

        public ChestDrop()
        {


            player = (Player)GameObject.Find("player").GetComponent(typeof(Player));
            sps = new ShotProjectile[player.character.weapons.Length];
            for(int i = 0; i < sps.Length; i++)
            {
                if (player.character.weapons[i] != null)
                {
                    sps[i] = (ShotProjectile) player.character.weapons[i].GetProjectile().GetComponent(typeof(ShotProjectile));
                }
            }
            PopulateUpgradeList();
            rewardIndices = new int[2];
            GenerateIndices();
        }

        private void PopulateUpgradeList()
        {
            //upgrades.Add(new Upgrade("Increase Projectile Count by 2!", ()=> player.mods.bulletMultiplier += 2));
            upgrades.Add(new Upgrade("Increase Weapon Damage by 50%!", () => player.mods.damageMod *= 1.5f));
            foreach(Upgrade upgrade in sps[0].GetUpgrades())
            {
                if (upgrade != null)
                {
                    upgrades.Add(upgrade);
                }
            }
            if (sps[1] == null)
            {
                GenerateWeapon();
                upgrades.Add(new Upgrade("Get " + newWeapon.gameObject.GetComponent<ShotProjectile>().GetName() + "!", () => player.character.weapons[1] = new Gun(newWeapon)));
            }else
            {
                foreach (Upgrade upgrade in sps[1].GetUpgrades())
                {
                    if (upgrade != null)
                    {
                        upgrades.Add(upgrade);
                    }
                }
            }

        }

        private void GenerateIndices()
        {
            Debug.Log(upgrades.Count);
            rewardIndices[0] = Random.Range(0, upgrades.Count);
            rewardIndices[1] = Random.Range(0, upgrades.Count);
            int loops = 50;
            while(rewardIndices[0] == rewardIndices[1] && loops > 0)
            {
                loops--;
                rewardIndices[1] = Random.Range(0, upgrades.Count);
            }
        }

        private void GenerateWeapon()
        {
            while (newWeapon == null)
            {
                //get random GO projectile from list
                int newWeaponInd = Random.Range(0, player.character.projectileArray.Length);
                //if random GO projectile is different from GO player.char.weapons[0].projectile  
                if (player.character.projectileArray[newWeaponInd] != player.character.weapons[0].projectile)
                {
                    //keep new GO projectile
                    newWeapon = player.character.projectileArray[newWeaponInd];
                }
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