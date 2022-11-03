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
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI currencyText;

    public TextMeshProUGUI gameOverText;
    public Button mainMenuButton;

    public bool isGameActive;

    public GameObject titleScreen;
    public GameObject pauseScreen;

    public List<GameObject> enemies;

    void Start()
    {
        Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //spawns enemies while game is running
    IEnumerable SpawnEnemy()
    {
        int enemyCount = 0;
        if(enemyCount <= 5)
        {
            yield return new WaitForSeconds(5);
            int index = Random.Range(0, enemies.Count);
            Instantiate(enemies[index]);
        }
    }

    public void UpdateCurrency(int currencyToAdd)
    {
        //add currency here
        currency += currency;
        currencyText.text = ": " + currency;
    }

    public void UpdateHealth(int healthToAdd)
    {
        //add health here
        health += healthToAdd;
        healthText.text = ": " + health;
        if(health <= 0)
        {
            GameOver();
        }
    }

    public void StartGame()
    {
        isGameActive = true;
        Time.timeScale = 1;
        currency = 0;
        health = 1;
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
}
