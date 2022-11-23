using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{

    private Button menuButton;
    private GameManager gameManager;

    void Start()
    {
        menuButton = GameObject.Find("MainMenuButton").GetComponent<Button>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        menuButton.onClick.AddListener(ReturnToMenu);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ReturnToMenu()
    {
        gameManager.TitleMenu();
        gameManager.UngeneratePlayer();
    }
}
