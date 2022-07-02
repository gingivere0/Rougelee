using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rougelee
{
    public class Gun
    {

        GameObject playerObject;
        GameObject crosshair;

        public GameObject projectile;
        ShotProjectile sp;

        float cooldown;
        float lastShot;

        public Gun(GameObject projectile)
        {
            this.projectile = projectile; 
            playerObject = GameObject.FindGameObjectWithTag("Player");
            crosshair = GameObject.FindGameObjectWithTag("Crosshair");
            lastShot = -1;
            cooldown = -1;
            sp = projectile.GetComponent<ShotProjectile>();
        }

        public GameObject GetProjectile()
        {
            return projectile;
        }

        //spawns ShotProjectile 1/4 of the way between the player and the crosshair
        //then moves projectile in the direction of the crosshair.
        //also shoots a fan of ShotProjectiles based on Modifier.bulletMultiplier.
        //to shoot a fan, get the angle from the player to the crosshair, add 10 degrees for every even
        //projectile fired, subtract 10 degrees for every odd bullet fired
        //rotate the projectiles in the direction it's headed
        //please don't touch this method
        public bool Shoot()
        {
            cooldown = sp.cooldown;
            if (Time.time - lastShot > cooldown)
            {
                Vector3 crosshairDirection = crosshair.transform.position - playerObject.transform.position;

                float rad2deg = 180 / (float)System.Math.PI;
                //get angle from player to crosshair
                double angle = System.Math.Atan2(crosshairDirection.y, crosshairDirection.x) * rad2deg;

                Vector3 spreadPosition = new Vector3();
                Player player = ((Player)playerObject.GetComponent(typeof(Player)));
                int degreeIncr = 0;
                int numProj = player.mods.bulletMultiplier;

                //if sword has the spinSword upgrade, the sword swings 36 times to form a circle
                if (sp.GetType().Name == "Sword" && UpgradeTree.spinSword)
                {
                    numProj = UpgradeTree.swordProj;
                }

                for (int bulletNum = 0; bulletNum < numProj; bulletNum++)
                {
                    //even bullets get 10 degrees added to their angle, odd bullets get 10 degrees subtracted
                    double bulletFlip = (System.Math.Pow((-1), (bulletNum + 1)));
                    //bullets 2 and 3 get +/-10 degrees, 4 and 5 get +/-20 degrees, etc.
                    if (bulletNum % 2 == 1)
                    {
                        degreeIncr++;
                    }
                    //idr why multiply by 4 but it works
                    spreadPosition.x = 4 * ((float)System.Math.Cos((angle + 10 * degreeIncr * bulletFlip) / rad2deg));
                    spreadPosition.y = 4 * ((float)System.Math.Sin((angle + 10 * degreeIncr * bulletFlip) / rad2deg));

                    //spawnlocation is 20% of the way from the player to the bullet's crosshair
                    Vector2 spawnLocation = (playerObject.transform.position * 4 + spreadPosition) / 4;

                    GameObject proj = Object.Instantiate(projectile, spawnLocation, Quaternion.identity);

                    //the bullet's target is its crosshair. its crosshair is at player position + its spreadPosition
                    Vector3 targ = playerObject.transform.position + spreadPosition;
                    targ.z = 0f;

                    //subtract the bullet's position because it's already included in spreadPosition (i think?)
                    Vector3 objectPos = proj.transform.position;
                    targ.x = targ.x - objectPos.x;
                    targ.y = targ.y - objectPos.y;

                    //rotate the bullet so that it's facing its target
                    float lookAngle = Mathf.Atan2(targ.y, targ.x) * Mathf.Rad2Deg;
                    proj.transform.rotation = Quaternion.Euler(new Vector3(0, 0, lookAngle));

                    ShotProjectile shotProjectile = proj.GetComponent<ShotProjectile>();
                    shotProjectile.setDamage(shotProjectile.getDamage() * player.mods.damageMod);

                    spreadPosition.Normalize();

                    shotProjectile.Shoot(spreadPosition);


                    proj.transform.parent = playerObject.transform.root.parent;
                }
                lastShot = Time.time;
                return true;
            }
            return false;
        }

        public float GetDamage()
        {
            return projectile.GetComponent<ShotProjectile>().damage;
        }
    }
}
