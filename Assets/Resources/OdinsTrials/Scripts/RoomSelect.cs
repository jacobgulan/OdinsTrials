using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace OdinsTrials 
{ 
    public class RoomSelect : MonoBehaviour
    {
        private bool isColliding;
        private int spawnedEnemies;
        private int lastNumber;
        private int EnemyType0, EnemyType1, EnemyType2, EnemyType3;
        private Collider2D collisionObj;

        public bool wait;

        public GameObject Skeleton;
        public GameObject Goblin;
        public GameObject Mushroom;
        public GameObject FlyingEye;

        ChangePreview  Preview0_0, Preview0_1, Preview1_0, Preview1_1;
        EnemySelector Enemies;
        PlayerBehaviour Player;

        // Start is called before the first frame update
        void Start()
        {
            //Player = FindObjectOfType<PlayerBehaviour>();
            transform.GetChild(0).gameObject.SetActive(false);

            
            wait = false;
            Enemies = FindObjectOfType<EnemySelector>();
            Player = FindObjectOfType<PlayerBehaviour>();

            /*
            Preview0_0 = GameObject.Find("Preview0_0").GetComponent<ChangePreview>();
            Preview0_1 = GameObject.Find("Preview0_1").GetComponent<ChangePreview>();

            Preview1_0 = GameObject.Find("Preview1_0").GetComponent<ChangePreview>();
            Preview1_1 = GameObject.Find("Preview1_1").GetComponent<ChangePreview>();

            if (gameObject.name == "RoomSelect0") {
                RandomizeEnemies();
            }
            */
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown("e"))
            {
                if (collisionObj != null)
                {
                    if (isColliding && collisionObj.gameObject.tag == "Player" && !wait)
                    {
                        if (gameObject.name == "RoomSelect0")
                        {
                            GameObject.Find("RoomSelect1").GetComponent<RoomSelect>().wait = true;
                            wait = true;
                        }
                        else if (gameObject.name == "RoomSelect1")
                        {
                            GameObject.Find("RoomSelect0").GetComponent<RoomSelect>().wait = true;
                            wait = true;
                        }
                        
                        transform.GetChild(0).gameObject.SetActive(true);

                        SpawnEnemies();
                    }
                }
            }
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            isColliding = true;
            collisionObj = other;
        }

        void OnTriggerExit2D(Collider2D other)
        {
            isColliding = false;
            collisionObj = null;
        }


        private void SpawnEnemies()
        {
            GameObject Enemy0, Enemy1;

           // Debug.Log("Enemy0 " + EnemyType0);
          //  Debug.Log("Enemy2 " + EnemyType2);

            Enemy1 = ChooseEnemy(Enemies.EnemyType3);

            // Primary enemy
            if (gameObject.name == "RoomSelect0")
            {
                Enemy0 = ChooseEnemy(Enemies.EnemyType0);
                if (Player.stagesCleared > 3)
                {
                    Enemy1 = ChooseEnemy(Enemies.EnemyType1);
                }
            }
            else
            {
                Enemy0 = ChooseEnemy(Enemies.EnemyType2);
                if (Player.stagesCleared > 3)
                {
                    Enemy1 = ChooseEnemy(Enemies.EnemyType3);
                }
            }

            GameObject spawn0, spawn1, spawn2;

            spawn0 = GameObject.Find("EnemySpawn0");
            spawn1 = GameObject.Find("EnemySpawn1");
            spawn2 = GameObject.Find("EnemySpawn2");

            int randSpawn, randNum;

            for (int i = 0; i < 6; i++)
            {
                randSpawn = Random.Range(0, 3);
                randNum = Random.Range(0, 2);

                // Pick random spawn location
                if (randSpawn == 0)
                {
                    if (Player.stagesCleared > 3)
                    {
                        if (randNum == 0)
                        {
                            Instantiate(Enemy0, new Vector2(spawn0.transform.position.x, spawn0.transform.position.y), Quaternion.identity);
                        }
                        else
                        {
                            Instantiate(Enemy1, new Vector2(spawn0.transform.position.x, spawn0.transform.position.y), Quaternion.identity);
                        }
                    } 
                    else
                    {
                        Instantiate(Enemy0, new Vector2(spawn0.transform.position.x, spawn0.transform.position.y), Quaternion.identity);
                    }
                }
                else if (randSpawn == 1)
                {
                    if (Player.stagesCleared > 3)
                    {
                        if (randNum == 0)
                        {
                            Instantiate(Enemy0, new Vector2(spawn1.transform.position.x, spawn1.transform.position.y), Quaternion.identity);
                        }
                        else
                        {
                            Instantiate(Enemy1, new Vector2(spawn1.transform.position.x, spawn1.transform.position.y), Quaternion.identity);
                        }
                    }
                    else
                    {
                        Instantiate(Enemy0, new Vector2(spawn1.transform.position.x, spawn1.transform.position.y), Quaternion.identity);
                    }
                }
                else if (randSpawn == 2)
                {
                    if (Player.stagesCleared > 3)
                    {
                        if (randNum == 0)
                        {
                            Instantiate(Enemy0, new Vector2(spawn2.transform.position.x, spawn2.transform.position.y), Quaternion.identity);
                        }
                        else
                        {
                            Instantiate(Enemy1, new Vector2(spawn2.transform.position.x, spawn2.transform.position.y), Quaternion.identity);
                        }
                    }
                    else
                    {
                        Instantiate(Enemy0, new Vector2(spawn2.transform.position.x, spawn2.transform.position.y), Quaternion.identity);
                    }
                }
            }

            
        }

        public void StageCleared()
        {
            transform.GetChild(0).gameObject.SetActive(false); 
            
            GameObject.Find("RoomSelect1").GetComponent<RoomSelect>().wait = false;
            GameObject.Find("RoomSelect0").GetComponent<RoomSelect>().wait = false;

            if (gameObject.name == "RoomSelect0") {
                Enemies.RandomizeEnemies();
            }
        }

        private GameObject ChooseEnemy(int EnemyNum)
        {
            if (EnemyNum == 0)
            {
                return Skeleton;
            }
            else if (EnemyNum == 1)
            {
                return Goblin;
            }
            else if (EnemyNum == 2)
            {
                return Mushroom;
            }
            else if (EnemyNum == 3)
            {
                return FlyingEye;
            }
            else
            {
                return null;
            }
        }

    }
}