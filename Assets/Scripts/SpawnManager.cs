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
        coinCount = GameObject.FindGameObjectsWithTag("CoinPickup").Length;
        //spawns enemies/others/coins if coint below number
        if (enemyCount < 3)
        {
            SpawnEnemies();
        }
        if(neutralCount < 5)
        {
            SpawnNeutralEntities();
        }
        if(coinCount < 10)
        {
            SpawnCoins();
        }

    }

    private Vector3 GenerateSpawnLocation()
    {
        //supposed to spawn outside camera range
        //0 to 1 is within view range
        float randomX = Random.Range(-2.0f, 2.0f);
        float randomY = Random.Range(-2.0f, 2.0f);
        if (randomX >= 0 && randomX <= 1)
        {
            randomX -= 1;
        }
        if (randomY >= 0 && randomY <= 1)
        {
            randomY -= 1;
        }

        Vector3 randPos = Camera.main.ViewportToWorldPoint(new Vector3(randomX, randomY, 0f));
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
