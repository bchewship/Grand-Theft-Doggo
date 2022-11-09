using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public GameObject[] neutralPrefabs;
    private float spawnRange;
    private int enemyCount;
    private int neutralCount;
    private int coinCount;
    public GameObject coinPrefab;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        neutralCount = GameObject.FindGameObjectsWithTag("Neutral").Length;
        //spawns enemies/others/coins if coint below number
        if(enemyCount < 3)
        {
            SpawnEnemies();
        }
        if(neutralCount < 5)
        {
            SpawnNeutralEntities();
        }
        coinCount = GameObject.FindGameObjectsWithTag("CoinPickup").Length;
        if(coinCount < 10)
        {
            SpawnCoins();
        }

    }

    private Vector3 GenerateSpawnLocation()
    {
        //supposed to spawn outside camera range
        //0 to 1 is within view range
        float random = Random.Range(-2.0f, 2.0f);
        if(random >=0 && random <= 1)
        {
            random -= 1;
        }
        
        Vector3 randPos = Camera.main.ViewportToWorldPoint(new Vector3(random, 0f, 0f));

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
