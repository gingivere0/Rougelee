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

        float cooldown;
        float lastShot;

        public Gun(GameObject projectile)
        {
            this.projectile = projectile; 
            playerObject = GameObject.FindGameObjectWithTag("Player");
            crosshair = GameObject.FindGameObjectWithTag("Crosshair");
            lastShot = -1;
            cooldown = -1;
        }

        public GameObject GetProjectile()
        {
            return projectile;
        }

        //spawns ShotProjectile 1/4 of the way between the player and the crosshair
        //then moves projectile in the direction of the crosshair.
        //also shoots a fan of ShotProjectiles based on Modifier.bulletMultiplier
        //please don't touch this method
        public bool Shoot()
        {
            cooldown = projectile.GetComponent<ShotProjectile>().cooldown;
            if (Time.time-lastShot > cooldown)
            {
                Vector3 crosshairDirection = crosshair.transform.position - playerObject.transform.position;

                float rad2deg = 180 / (float)System.Math.PI;
                double angle = System.Math.Atan2(crosshairDirection.y, crosshairDirection.x) * rad2deg;

                Vector3 spreadPosition = new Vector3();
                Player player = ((Player)playerObject.GetComponent(typeof(Player)));
                int degreeIncr = 0;
                
                for (int bulletNum = 0; bulletNum < player.mods.bulletMultiplier; bulletNum++)
                {
                    double bulletFlip = (System.Math.Pow((-1), (bulletNum + 1)));
                    if (bulletNum % 2 == 1)
                    {
                        degreeIncr++;
                    }
                    spreadPosition.x = 4 * ((float)System.Math.Cos((angle + 10 * degreeIncr * bulletFlip) / rad2deg));
                    spreadPosition.y = 4 * ((float)System.Math.Sin((angle + 10 * degreeIncr * bulletFlip) / rad2deg));

                    Vector2 spawnLocation = (playerObject.transform.position * 4 + spreadPosition) / 4;

                    GameObject proj = Object.Instantiate(projectile, spawnLocation, Quaternion.identity);

                    Vector3 targ = playerObject.transform.position + spreadPosition;
                    targ.z = 0f;

                    Vector3 objectPos = proj.transform.position;
                    targ.x = targ.x - objectPos.x;
                    targ.y = targ.y - objectPos.y;

                    float lookAngle = Mathf.Atan2(targ.y, targ.x) * Mathf.Rad2Deg;
                    proj.transform.rotation = Quaternion.Euler(new Vector3(0, 0, lookAngle));

                    //ShotProjectile shotProjectile = (ShotProjectile)proj.GetComponent(typeof(ShotProjectile));
                    ShotProjectile shotProjectile = proj.GetComponent<ShotProjectile>();
                    shotProjectile.setDamage(shotProjectile.getDamage() * player.mods.damageMod);

                    spreadPosition.Normalize();
                    //shotProjectile.Shoot(bulletNum,direction);

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
