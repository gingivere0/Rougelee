using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rougelee
{
    public class Gun
    {

        GameObject playerObject;
        GameObject crosshair;

        GameObject projectile;

        public Gun(GameObject projectile)
        {
            this.projectile = projectile; 
            playerObject = GameObject.FindGameObjectWithTag("Player");
            crosshair = GameObject.FindGameObjectWithTag("Crosshair");

        }

        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void Shoot()
        {
            //spawn 1/3 between player and crosshair

            Vector3 crosshairDirection = crosshair.transform.position-playerObject.transform.position;

            float rad2deg = 180 / (float)System.Math.PI;
            double angle = System.Math.Atan2(crosshairDirection.y, crosshairDirection.x) * rad2deg;

            angle = System.Math.Atan2(crosshairDirection.y, crosshairDirection.x) * rad2deg;

            //direction.Normalize();
            Vector3 spreadPosition = new Vector3();
            Player player = ((Player)playerObject.GetComponent(typeof(Player)));
            int degreeIncr = 0;
            for (int bulletNum = 0; bulletNum < player.mods.bulletMultiplier; bulletNum++) {
                double bulletFlip = (System.Math.Pow((-1) , (bulletNum + 1)));
                if(bulletNum%2 == 1)
                {
                    degreeIncr++;
                }
                spreadPosition.x = 4*((float)System.Math.Cos((angle + 10 * degreeIncr*bulletFlip) / rad2deg));
                spreadPosition.y = 4*((float)System.Math.Sin((angle + 10 * degreeIncr*bulletFlip) / rad2deg));
                //direction*new Vector3(, (float)System.Math.Sin((angle + 10 * bulletNum) / rad2deg),0);
                //Debug.Log("sprpos: " + spreadPosition);
                //Debug.Log("playerPos: " + player.transform.position);
                //angle = System.Math.Atan2(spreadPosition.y, spreadPosition.x) * rad2deg;
                //Debug.Log("newAngle: " + angle);

                Vector2 spawnLocation = (playerObject.transform.position*4+spreadPosition) / 4;

                //Debug.Log("sploc: "+spawnLocation);

                //spawnLocation.x *= (float) System.Math.Cos(10 * i * rad2deg);
                //spawnLocation.y *= (float) System.Math.Sin(10 * i * rad2deg);

                //double angle = System.Math.Atan2(spawnLocation.y, spawnLocation.x) * rad2deg;

                //spawnLocation = new Vector2((float)System.Math.Cos((angle + 10 * bulletNum) / rad2deg), (float)System.Math.Sin((angle + 10 * bulletNum) / rad2deg));



                GameObject proj = MonoBehaviour.Instantiate(projectile, spawnLocation, Quaternion.identity);

                Vector3 targ = playerObject.transform.position+spreadPosition;
                targ.z = 0f;

                Vector3 objectPos = proj.transform.position;
                targ.x = targ.x - objectPos.x;
                targ.y = targ.y - objectPos.y;

                float lookAngle = Mathf.Atan2(targ.y, targ.x) * Mathf.Rad2Deg;
                proj.transform.rotation = Quaternion.Euler(new Vector3(0, 0, lookAngle));

                ShotProjectile shotProjectile = (ShotProjectile)proj.GetComponent(typeof(ShotProjectile));
                shotProjectile.setDamage(shotProjectile.getDamage() * player.mods.damageMod);

                spreadPosition.Normalize();
                //shotProjectile.Shoot(bulletNum,direction);
                shotProjectile.Shoot(spreadPosition);


                proj.transform.parent = playerObject.transform.root.parent;
            }
        }
    }
}
