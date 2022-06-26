using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rougelee
{
    public class ChestDrop
    {
        int[] rewardIndices = new int[2];
        int startInd = 0;
        Player player;
        GameObject newWeapon = null;

        public ChestDrop()
        {


            player = (Player)GameObject.Find("player").GetComponent(typeof(Player));
            //need list of GameObject projectiles. pick one from list at random. 
            //if GO projectile is the same as the player.char.weapon[0].projectile, repeat
            //if GO projectile is different, keep new GO projectile,
            //and set player.char.weapon[1] = new Gun(GO projectile)

            
            if (player.character.weapons[1] == null)
            {
                GenerateWeapon();
            }
            else
            {
                startInd=1;
            }
            GenerateIndices();
        }

        private void GenerateIndices()
        {
            rewardIndices[0] = Random.Range(startInd, 3);
            rewardIndices[1] = Random.Range(startInd, 3);
            while(rewardIndices[0] == rewardIndices[1])
            {
                rewardIndices[1] = Random.Range(startInd, 3);
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
            if (rewardIndices[rewardInd] == 0)
            {
                player.character.weapons[1] = new Gun(newWeapon);
            } else if (rewardIndices[rewardInd] == 1)
            {
                player.mods.bulletMultiplier += 2;
            } else if (rewardIndices[rewardInd] == 2)
            {
                player.mods.damageMod *= 1.5f;
            }
        }
        public virtual string GetText(int rewardInd)
        {
            if(rewardIndices[rewardInd] == 0)
            {
                return "Get "+newWeapon.gameObject.GetComponent<ShotProjectile>().GetName()+"!";
            }else if (rewardIndices[rewardInd] == 1)
            {
                return "Increase projectile count by 2!";
            }else if (rewardIndices[rewardInd] == 2)
            {
                return "Increase weapon damage by 50%";
            }
            return "this is a test";
        }
    }
}