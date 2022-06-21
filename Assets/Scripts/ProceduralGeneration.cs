using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rougelee
{
    public class ProceduralGeneration : MonoBehaviour
    {

        public int width, height;
        public GameObject ground1, ground2;

        public GameObject[] enemyList;


        GameObject player;
        Vector2 targetPos;

        float enemyXSpawn, enemyYSpawn;

        int once = 0;

        // Start is called before the first frame update
        void Start()
        {
            Camera cam = Camera.main;
            enemyXSpawn = cam.orthographicSize * cam.aspect + 1;
            enemyYSpawn = cam.orthographicSize + 1;
            player = GameObject.FindGameObjectWithTag("Player");
            ground1.transform.localScale = new Vector3(1.563623f, 1.563623f, 1f);//scales the image to tessalate
            ground2.transform.localScale = new Vector3(1.563623f, 1.563623f, 1f);
            Generation();
        }

        // TODO: GET RID OF THIS IF
        // Update is called once per frame
        void Update()
        {
            if (once == 0)
            {
                generateMooses();
            }
        }

        /*
         * Generates mooses until there are enemyNum of them. Spawns mooses 
         * out of frame on random locations along each edge of the camera.
         */
        void generateMooses()
        {
            System.Random rand = new System.Random();
            int quadrant;
            int maxEnemies = 20;
            targetPos = player.transform.position;
            for (int numEnemies = GameObject.FindGameObjectsWithTag("Enemy").Length; numEnemies < maxEnemies; numEnemies++)
            {
                int enemyInd = rand.Next(0, enemyList.Length);
                Debug.Log(enemyList.Length);
                quadrant = (int)(rand.NextDouble() * 4);
                if (quadrant == 0)
                {
                    //spawnObj(moose, (int)enemyXSpawn, (int)enemyYSpawn);
                    spawnObj(enemyList[enemyInd], (int)((rand.NextDouble() * enemyXSpawn) + (targetPos.x - (.5 * enemyXSpawn))), (int)(enemyYSpawn + targetPos.y));
                }
                else if (quadrant == 1)
                {
                    spawnObj(enemyList[enemyInd], (int)(enemyXSpawn + targetPos.x), (int)((rand.NextDouble() * enemyYSpawn) + (targetPos.y - (.5 * enemyYSpawn))));
                }
                else if (quadrant == 2)
                {
                    spawnObj(enemyList[enemyInd], (int)((rand.NextDouble() * enemyXSpawn) + (targetPos.x - (.5 * enemyXSpawn))), (int)((-1 * enemyYSpawn) + targetPos.y));
                }
                else if (quadrant == 3)
                {
                    spawnObj(enemyList[enemyInd], (int)((-1 * enemyXSpawn) + targetPos.x), (int)((rand.NextDouble() * enemyYSpawn) + (targetPos.y - (.5 * enemyYSpawn))));
                }
            }
        }

        void Generation()
        {

            //starting at the negative half the width and negative half the height from the origin, spawns width by height size grid of dungeon floors, with dungeon floors2 every 10 blocks
            for (int x = (int)(-1 * (width / 2)); x < (int)(width / 2); x++)
            {
                for (int y = (int)(-1 * (height / 2)); y < (int)(height / 2); y++)
                {
                    if (x % 10 == 0 || y % 10 == 0)
                    {
                        spawnObj(ground2, x, y);
                    }
                    else
                    {
                        spawnObj(ground1, x, y);
                    }
                }
            }

        }

        void spawnObj(GameObject obj, int x, int y)
        {
            obj = Instantiate(obj, new Vector2(x, y), Quaternion.identity);
            obj.transform.parent = this.transform;
            

            //want to start enemies already looking at the player
            if (obj.tag == "Enemy")
            {
                targetPos = player.transform.position;
                Vector3 vectorToTarget = targetPos - (Vector2)obj.transform.position;
                float angle = (Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg) - 90;
                Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
                obj.transform.rotation = Quaternion.RotateTowards(obj.transform.rotation, q, 10000f);

            }
        }

    }
}