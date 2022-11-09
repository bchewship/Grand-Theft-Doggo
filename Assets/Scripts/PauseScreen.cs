using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseScreen : MonoBehaviour
{
    private Button resumeButton;
    private Button menuButton;
    private Button quitButton;
    private GameManager gameManager;

    void Start()
    {
        resumeButton = GameObject.Find("ResumeButton").GetComponent<Button>();
        menuButton = GameObject.Find("MenuButton").GetComponent<Button>();
        quitButton = GameObject.Find("QuitButton").GetComponent<Button>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        resumeButton.onClick.AddListener(Resume);
        menuButton.onClick.AddListener(ReturnToMenu);
        quitButton.onClick.AddListener(ExitGame);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Resume()
    {
        gameManager.ResumeGame();
    }

    void ReturnToMenu()
    {
        gameManager.TitleMenu();
        gameManager.UngeneratePlayer();
    }

    void ExitGame()
    {
        Application.Quit();
    }
}
