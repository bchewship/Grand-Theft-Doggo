using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public GameObject[] neutralPrefabs;
    private int enemyCount;
    private int neutralCount;
    private int coinCount;
    public GameObject coinPrefab;

    private GameObject player;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        neutralCount = GameObject.FindGameObjectsWithTag("Neutral").Length;
        coinCount = GameObject.FindGameObjectsWithTag("CoinPickup").Length;
        player = GameObject.Find("Player");
        //spawns enemies/others/coins if coint below number
        if (enemyCount < 10)
        {
            SpawnEnemies();
        }
        if(neutralCount < 15)
        {
            SpawnNeutralEntities();
        }
        if(coinCount < 5)
        {
            SpawnCoins();
        }

    }

    private Vector3 GenerateSpawnLocation()
    {
        float randomX = Random.Range(-100.0f, 100.0f);
        float randomZ = Random.Range(-100.0f, 100.0f);
        int rand = Random.Range(0, 2);
        //if spawn would have been within view range, then randomly decide to move spawn point + or -
        if (randomX >= -20 && randomX <= 20)
        {
            if (rand == 0)
            {
                randomX -= 40;
            }
            else if (rand == 1)
            {
                randomX += 40;
            }

        }
        if (randomZ >= -20 && randomZ <= 20)
        {
            if (rand == 0)
            {
                randomZ -= 40;
            }
            else if (rand == 1)
            {
                randomZ += 40;
            }

        }

        //Vector3 randPos = Camera.main.ViewportToWorldPoint(new Vector3(randomX, randomZ, 1f));
        //changed to spawn based on player location vs camera location, since it would spawn high in the air
        Vector3 randPos = new Vector3(player.transform.position.x + randomX, 1f, player.transform.position.z + randomZ);
        return randPos;
    }


    void SpawnEnemies()
    {
        GameObject enemy = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
        Instantiate(enemy, GenerateSpawnLocation(), enemy.transform.rotation);
    }

    void SpawnNeutralEntities()
    {
        GameObject neutral = neutralPrefabs[Random.Range(0, neutralPrefabs.Length)];
        Instantiate(neutral, GenerateSpawnLocation(), neutral.transform.rotation);
    }
    void SpawnCoins()
    {
        Instantiate(coinPrefab, GenerateSpawnLocation(), coinPrefab.transform.rotation);
    }

}
