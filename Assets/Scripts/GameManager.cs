using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private int currency;
    private int health;
    private int maxHealth;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI currencyText;

    public TextMeshProUGUI gameOverText;
    public Button mainMenuButton;

    public bool isGameActive;

    public GameObject titleScreen;
    public GameObject pauseScreen;

    public GameObject[] player;
    private int index;

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
        healthText.text = ": " + health + "/" + maxHealth;
        if(health < maxHealth)
        {
            health += healthToAdd;
        }
        else if(health >= maxHealth)
        {
            health = maxHealth;
            health += healthToAdd;
        }
        if(health <= 0)
        {
            GameOver();
            Debug.Log("byby");
        }
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
    }

    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        isGameActive = false;
        mainMenuButton.gameObject.SetActive(true);
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
    }
    
    void GeneratePlayer()
    {
        //creates a random player character
        index = Random.Range(0, player.Length);
        player[index].SetActive(true);
    }

    public void UngeneratePlayer()
    {
        //unselects the random player character
        player[index].SetActive(false);
    }
        
}
