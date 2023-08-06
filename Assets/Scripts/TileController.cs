using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileController : MonoBehaviour
{
    [Header("Tile Settings")]
    public GameObject[] tilePrefabs;
    public float zSpawn = 0;
    public float tileLength = 13; //25.25
    public float tileSpeed;
    public int numberOfTiles, numberOfObstacles;
    public List<GameObject> activeTiles = new List<GameObject>();


    [Header("Obstacles")]
    public GameObject[] obstaclePrefabs;
    public List<GameObject> activeObstacles = new List<GameObject>();
    public float obstacleSpeed;
    public float timeCounterForObstacleSpawn;
    private int keeper;
    private float[] numbers = {4.7f, 2.7f, 0.7f};
   // private int randomIndex = Random.Range(0, 3);
   // private float randomFloatFromNumbers = numbers[randomIndex];


    [Header("Others")]
    public static float score=0;

    public Transform playerTransform;
    void Start()
    {
        //Debug.Log(activeObstacles.Count);
        score = 0;
        keeper = (int)timeCounterForObstacleSpawn;
        //GameObject spawnedTile;
        for(int i = 0; i < numberOfTiles; i++)
        {
            /*
            if (i == 0)
            {
                spawnedTile = Instantiate(tilePrefabs[0], transform.forward * zSpawn , transform.rotation);

            }
            else
            {
                spawnedTile = Instantiate(tilePrefabs[Random.Range(0, tilePrefabs.Length)],transform.forward * zSpawn , transform.rotation);
                
            }          
            activeTiles.Add(spawnedTile);
            */
            //zSpawn += tileLength;  //176.75
        }
        for (int i = 0; i < numberOfObstacles; i++)
        {
            int randomIndex = Random.Range(0, 3);
            float randomFloatFromNumbers = numbers[randomIndex];
            activeObstacles[i].transform.position = new Vector3(-randomFloatFromNumbers, activeObstacles[i].transform.position.y, activeObstacles[i].transform.position.z);
            //SpawnObstacle(Random.Range(0, obstaclePrefabs.Length));
        }

    }

    // Update is called once per frame
    void Update()
    {

        if (!PlayerManager.gameOver)
        {
            if (!PlayerManager.startGamePanel)
            {
                score += Time.deltaTime;
                timeCounterForObstacleSpawn -= Time.deltaTime;
                if (timeCounterForObstacleSpawn <= 0 && numberOfObstacles < activeObstacles.Count)
                {
                    //SpawnObstacle(Random.Range(0, obstaclePrefabs.Length));
                    numberOfObstacles++;
                    timeCounterForObstacleSpawn = keeper;
                }
                for (int i = 0; i < numberOfTiles; i++)
                {
                    activeTiles[i].transform.Translate((-transform.forward) * Time.deltaTime * (tileSpeed + (score / 10)));

                    if (activeTiles[i].transform.position.z + tileLength -3 < (playerTransform.position.z)) // "-3" I wrote. ??
                    {
                        activeTiles[i].transform.position = new Vector3(0, 0, 3 + (zSpawn - tileLength));
                        // Teleporting current tile.
                    }
                }
                for (int i = 0; i < numberOfObstacles; i++)
                {
                    activeObstacles[i].transform.Translate((transform.forward) * Time.deltaTime * (obstacleSpeed + (score / 10)));
                    if (activeObstacles[i].transform.position.z + tileLength - 25 < (playerTransform.position.z))
                    {
                        int randomIndex = Random.Range(0, 3); //?
                        float randomFloatFromNumbers = numbers[randomIndex];
                        //Debug.Log(randomFloatFromNumbers);
                        activeObstacles[i].transform.position = new Vector3(-randomFloatFromNumbers, -1.35f, (zSpawn - tileLength));
                        // Teleporting current obstacle.
                    }
                }


            }
        }


    }
    public void SpawnTile(int tileIndex)
    {
        GameObject go = Instantiate(tilePrefabs[tileIndex],transform.forward * zSpawn,transform.rotation);
        activeTiles.Add(go);
        zSpawn+=tileLength;
    }
    public void SpawnObstacle(int obstacleIndex)
    {

        // Choose a random point to spawn the obstacle.
        Vector3 spawnPoint = new Vector3(Random.Range(-4, -1), -1.35f, (Random.Range((zSpawn - tileLength) -40 , 20 + (zSpawn - tileLength))));
        Quaternion rotato = new Quaternion(0, Random.Range(0,180), 0, 0);


        //Spawn the obstacle at the position
        GameObject go = Instantiate(obstaclePrefabs[obstacleIndex],spawnPoint, rotato);
        activeObstacles.Add(go);
    }
}
