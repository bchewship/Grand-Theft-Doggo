using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;
//for data persistence
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameManager : MonoBehaviour
{
    public static GameManager manager;

    private int currency;
    private int health;
    private int maxHealth;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI currencyText;

    public bool isGameActive;

    public GameObject gameOverScreen;
    public GameObject titleScreen;
    public GameObject pauseScreen;

    public GameObject[] player;
    private int index;

    private Vector3 startingPos;

    void Awake()
    {
        if(manager == null)
        {
            DontDestroyOnLoad(gameObject);
            manager = this;
        }
        else if(manager != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        //pauses time until a button is clicked on title screen
        Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void UpdateCurrency(int currencyToAdd)
    {
        currency += currencyToAdd;
        currencyText.text = ": " + currency;
    }

    public void UpdateHealth(int healthToAdd)
    {
        health += healthToAdd;
        
        if(health <= 0)
        {
            GameOver();
        }
        //fixes health showing quantities above maxHealth
        health = Mathf.Clamp(health, 0, maxHealth);

        healthText.text = ": " + health + "/" + maxHealth;
    }

    public void StartGame()
    {
        GeneratePlayer();
        isGameActive = true;
        Time.timeScale = 1;
        currency = 0;
        health = 2;
        maxHealth = 2;
        titleScreen.gameObject.SetActive(false);
        UpdateCurrency(0);
        UpdateHealth(0);

        startingPos = player[index].transform.position;

    }

    public void GameOver()
    {
        Time.timeScale = 0;
        isGameActive = false;
        gameOverScreen.gameObject.SetActive(true);
        if (File.Exists(Application.persistentDataPath + "playerInfo.dat"))
        {
            //deletes save file on game over
            File.Delete(Application.persistentDataPath + "playerInfo.dat");
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        pauseScreen.gameObject.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        pauseScreen.gameObject.SetActive(false);
    }

    public void TitleMenu()
    {
        Time.timeScale = 0;
        titleScreen.gameObject.SetActive(true);
        pauseScreen.gameObject.SetActive(false);
        gameOverScreen.gameObject.SetActive(false);
    }
    
    void GeneratePlayer()
    {
        //creates a random player character
        index = Random.Range(0, player.Length);
        player[index].SetActive(true);
        player[index].transform.position = startingPos;
    }

    public void UngeneratePlayer()
    {
        player[index].transform.position = startingPos;
        //unselects the random player character
        player[index].SetActive(false);
    }
        
    public void Save()
    {
        //makes file binary for more security
        BinaryFormatter bf = new BinaryFormatter();
        //creates file for saving
        FileStream file = File.Create(Application.persistentDataPath + "playerInfo.dat");

        //the data to be saved
        PlayerData data = new PlayerData();
        data.health = health;
        data.maxHealth = maxHealth;
        data.currency = currency;
        data.index = index;

        //puts the data into the file
        bf.Serialize(file, data);
        file.Close();
    }

    public void Load()
    {
        if(File.Exists(Application.persistentDataPath + "playerInfo.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "playerInfo.dat", FileMode.Open);
            //pulls the data back out of the file
            PlayerData data = (PlayerData)bf.Deserialize(file);
            file.Close();

            //sets player stats to what was saved in the file
            health = data.health;
            maxHealth = data.maxHealth;
            currency = data.currency;
            index = data.index;

            Time.timeScale = 1;
            titleScreen.gameObject.SetActive(false);
            player[index].SetActive(true);
            UpdateCurrency(0);
            UpdateHealth(0);
        }
    }

}

//lets player stats be saved
[Serializable]
class PlayerData
{
    public int health;
    public int maxHealth;
    public int currency;
    public int index;

    //private Vector3 position;
    //private Quaternion rotation;
}
