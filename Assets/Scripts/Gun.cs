using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rougelee
{
    public class Gun
    {

        GameObject player;
        GameObject crosshair;

        GameObject projectile;

        public Gun(GameObject projectile)
        {
            this.projectile = projectile; 
            player = GameObject.FindGameObjectWithTag("Player");
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
            Vector3 spawnLocation = (player.transform.position + player.transform.position + crosshair.transform.position) / 3;

            GameObject proj = MonoBehaviour.Instantiate(projectile, spawnLocation, Quaternion.identity);

            proj.transform.parent = player.transform.root.parent;
        }
    }
}
