using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rougelee
{
    public class ProceduralGeneration : MonoBehaviour
    {

        public int width, height;
        public Biomes biomes;

        GameObject[] enemyList;
        GameObject[] currentEnemies;
        GameObject[] currentTiles;

        int availableEnemies = 0;
        public int level;


        GameObject playerObject;
        Vector2 targetPos;


        Vector2 lastPos;

        float enemyXSpawn, enemyYSpawn;

        public static bool spawnBoss;

        public static bool increaseLevel;



        // Start is called before the first frame update
        void Start()
        {
            Camera cam = Camera.main;
            enemyXSpawn = cam.orthographicSize * cam.aspect + 1;
            enemyYSpawn = cam.orthographicSize * cam.aspect + 1;
            playerObject = GameObject.FindGameObjectWithTag("Player");

            lastPos = new Vector2(0, 0);
            if (level == 0)
            {
                enemyList = biomes.dungeonEnemy;
                GenerateDungeon(0,0);
            }
            else if (level == 1)
            {
                enemyList = biomes.forestEnemy;
                GenerateForest(0,0);
            }
            else
            {
                enemyList = biomes.desertEnemy;
                GenerateDesert(0, 0);
            }
        }

        // Update is called once per frame
        void Update()
        {
            GenerateEnemies();
            MoveWithPlayer();
            if (spawnBoss)
            {
                SpawnBoss();
                spawnBoss = false;
                UIManager.spawned = true;
            }
            if (increaseLevel)
            {
                increaseLevel = false;
                level++;
                currentEnemies = GameObject.FindGameObjectsWithTag("Enemy");
                currentTiles = GameObject.FindGameObjectsWithTag("Floor");
                foreach (GameObject enemy in currentEnemies)
                {
                    Destroy(enemy);
                }
                foreach (GameObject floor in currentTiles)
                {
                    Destroy(floor);
                }
                if (level == 0)
                {
                    enemyList = biomes.dungeonEnemy;
                    GenerateDungeon(0,0);
                }
                else if (level == 1)
                {
                    enemyList = biomes.forestEnemy;
                    GenerateForest(0,0);
                }
                else
                {
                    enemyList = biomes.desertEnemy;
                    GenerateDesert(0, 0);
                }
            }
        }


        void SpawnBoss()
        {
            if (level == 0)
            {
                int bossInd = availableEnemies;
                if(availableEnemies > biomes.dungeonBoss.Length-1)
                {
                    bossInd = biomes.dungeonBoss.Length - 1;
                }
                spawnObj(biomes.dungeonBoss[bossInd],(int)(playerObject.transform.position.x + enemyXSpawn), (int)playerObject.transform.position.y);
            } else if (level == 1)
            {
                int bossInd = availableEnemies;
                if (availableEnemies > biomes.forestBoss.Length-1)
                {
                    bossInd = biomes.forestBoss.Length - 1;
                }
                spawnObj(biomes.forestBoss[bossInd], (int)(playerObject.transform.position.x + enemyXSpawn), (int)playerObject.transform.position.y);
            } else if (level == 2)
            {
                int bossInd = availableEnemies;
                if (availableEnemies > biomes.desertBoss.Length-1)
                {
                    bossInd = biomes.desertBoss.Length - 1;
                }
                spawnObj(biomes.desertBoss[bossInd], (int)(playerObject.transform.position.x + enemyXSpawn), (int)playerObject.transform.position.y);
            }
        }


        //pulls an offscreen piece from behind the player and places it in front of them
        //if a piece can't be found to move, spawns a new piece
        void MoveWithPlayer()
        {
            string pieceName = "land.0.0";
            GameObject movingpiece;
            Vector2 newPieceLocation;

            //pull a row from below and place it above
            if ((int)playerObject.transform.position.y > (int)lastPos.y)
            {
                lastPos.y = (int)playerObject.transform.position.y;

                for (int i = (-1 * width / 2) + (int)lastPos.x; i < width / 2 + 1 + (int)lastPos.x; i++)
                {

                    int xloc = i;
                    int yloc = (int)lastPos.y - (height / 2) - 1;

                    pieceName = "land." + xloc + "." + yloc;
                    movingpiece = GameObject.Find(pieceName);
                    if (movingpiece == null)
                    {
                        GenerateTile(xloc, yloc + height);
                    }
                    else
                    {
                        int newy = (int)movingpiece.transform.position.y + height;
                        pieceName = "land." + xloc + "." + newy;
                        movingpiece.name = pieceName;
                        newPieceLocation = new Vector2(xloc, newy);
                        movingpiece.transform.position = newPieceLocation;
                    }
                }

            }
            //pull a row from above and place it below
            else if ((int)playerObject.transform.position.y < (int)lastPos.y)
            {
                lastPos.y = (int)playerObject.transform.position.y;

                for (int i = (-1 * width / 2) + (int)lastPos.x; i < width / 2 + 1 + (int)lastPos.x; i++)
                {

                    int xloc = i;
                    int yloc = (int)lastPos.y + (height / 2);

                    pieceName = "land." + xloc + "." + yloc;
                    movingpiece = GameObject.Find(pieceName);
                    if (movingpiece == null)
                    {
                        GenerateTile(xloc, yloc - height);
                    }
                    else
                    {
                        int newy = (int)movingpiece.transform.position.y - height;
                        pieceName = "land." + xloc + "." + newy;
                        movingpiece.name = pieceName;
                        newPieceLocation = new Vector2(xloc, newy);
                        movingpiece.transform.position = newPieceLocation;
                    }
                }
            }
            movingpiece = null;

            //pull a row from the left and place it on the right
            if((int) playerObject.transform.position.x > (int)lastPos.x)
            {
                lastPos.x = (int)playerObject.transform.position.x;

                for(int i = (-1*height/2)+(int)lastPos.y; i < height / 2 + 1 + (int)lastPos.y; i++)
                {
                    int xloc = (int)lastPos.x-(width/2)-1;
                    int yloc = i;

                    pieceName = "land." + xloc + "." + yloc;
                    movingpiece = GameObject.Find(pieceName);
                    if (movingpiece == null)
                    {
                        GenerateTile(xloc + width, yloc);
                    }
                    else
                    {
                        int newx = (int)movingpiece.transform.position.x + width;
                        pieceName = "land." + newx + "." + yloc;
                        movingpiece.name = pieceName;
                        newPieceLocation = new Vector2(newx, yloc);
                        movingpiece.transform.position = newPieceLocation;
                    }
                }
            }
            //pull a row from the right and place it on the left
            else if ((int)playerObject.transform.position.x < (int)lastPos.x)
            {
                lastPos.x = (int)playerObject.transform.position.x;

                for (int i = (-1 * height / 2) + (int)lastPos.y; i < height / 2 + 1 + (int)lastPos.y; i++)
                {
                    int xloc = (int)lastPos.x + (width / 2);
                    int yloc = i;

                    pieceName = "land." + xloc + "." + yloc;
                    movingpiece = GameObject.Find(pieceName);
                    if (movingpiece == null)
                    {
                        GenerateTile(xloc - width, yloc);
                    }
                    else
                    {
                        int newx = (int)movingpiece.transform.position.x - width;
                        pieceName = "land." + newx + "." + yloc;
                        movingpiece.name = pieceName;
                        newPieceLocation = new Vector2(newx, yloc);
                        movingpiece.transform.position = newPieceLocation;
                    }
                }
            }
        }

        void GenerateTile(int x, int y)
        {
            if (level == 0)
            {
                GenerateDungeonTile(x, y);
            }
            else if (level == 1)
            {
                GenerateForestTile(x, y);
            }
            else
            {
                GenerateDesertTile(x, y);
            }
        }

        /*
         * Generates enemies until there are enemyNum of them. Spawns enemies 
         * out of frame on random locations along each edge of the camera.
         */
        void GenerateEnemies()
        {
            System.Random rand = new System.Random();
            int quadrant;
            int maxEnemies = 20;
            targetPos = playerObject.transform.position;
            for (int numEnemies = GameObject.FindGameObjectsWithTag("Enemy").Length; numEnemies < maxEnemies; numEnemies++)
            {
                availableEnemies = UIManager.minutes + 1;
                int enemyInd = rand.Next(0, availableEnemies>enemyList.Length?enemyList.Length:availableEnemies);
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

        void GenerateDungeonTile(int x, int y)
        {
            if (x % 10 == 0 || y % 10 == 0)
            {
                spawnObj(biomes.dungeonFloor[1], x, y);
            }
            else
            {
                spawnObj(biomes.dungeonFloor[0], x, y);
            }
        }

        void GenerateDungeon(int startx, int starty)
        {

            //starting at the negative half the width and negative half the height from the origin, spawns width by height size grid of dungeon floors, with dungeon floors2 every 10 blocks
            for (int x = (int)(-1 * (width / 2)+startx); x < (int)(width / 2)+startx; x++)
            {
                for (int y = (int)(-1 * (height / 2)+starty); y < (int)(height / 2)+starty; y++)
                {
                    GenerateDungeonTile(x, y);
                }
            }

        }

        void GenerateDesertTile(int x, int y)
        {
            System.Random rand = new System.Random();
            int randomDeserttileIndex = rand.Next(0, biomes.desertFloor.Length-1);
            if(rand.Next(0,40) == 0)
            {
                randomDeserttileIndex = 2;
            }
            spawnObj(biomes.desertFloor[randomDeserttileIndex], x, y);
            
        }

        void GenerateDesert(int startx, int starty)
        {

            //starting at the negative half the width and negative half the height from the origin, spawns width by height size grid of dungeon floors, with dungeon floors2 every 10 blocks
            for (int x = (int)(-1 * (width / 2) + startx); x < (int)(width / 2) + startx; x++)
            {
                for (int y = (int)(-1 * (height / 2) + starty); y < (int)(height / 2) + starty; y++)
                {
                    GenerateDesertTile(x, y);
                }
            }

        }

        // spawns single forest tile at position (x,y)
        void GenerateForestTile(int x,int y)
        {
            System.Random rand = new System.Random();
            if ((x % 5 == 0 || y % 5 == 0) && rand.Next(0, 3) == 0)
            {
                int randomForesttileIndex = rand.Next(1, biomes.forestFloor.Length);
                spawnObj(biomes.forestFloor[randomForesttileIndex], x, y);
            }
            else
            {
                spawnObj(biomes.forestFloor[0], x, y);
            }
        }

        void GenerateForest(int startx, int starty)
        {
            //starting at the negative half the width and negative half the height from (startx, starty), spawns width by height size grid of dungeon floors, with dungeon floors2 every 10 blocks
            for (int x = (int)(-1 * (width / 2)+startx); x < (int)(width / 2)+startx; x++)
            {
                for (int y = (int)(-1 * (height / 2)+starty); y < (int)(height / 2)+starty; y++)
                {
                    GenerateForestTile(x,y);
                }
            }
        }

        void spawnObj(GameObject obj, int x, int y)
        {
            obj = Instantiate(obj, new Vector2(x, y), Quaternion.identity);
            obj.transform.parent = this.transform.GetChild(0);

            //want to start enemies already looking at the player
            if (obj.tag == "Enemy")
            {
                obj.transform.parent = this.transform.GetChild(1);


                targetPos = playerObject.transform.position;
                Vector3 vectorToTarget = targetPos - (Vector2)obj.transform.position;
                float angle = (Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg) - 90;
                Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
                obj.transform.rotation = Quaternion.RotateTowards(obj.transform.rotation, q, 10000f);

            }else if(obj.tag == "Boss")
            {

                obj.transform.parent = this.transform.GetChild(1);
            }
            else
            {
                obj.name = "land."+x+"."+y;
            }
        }

    }
}