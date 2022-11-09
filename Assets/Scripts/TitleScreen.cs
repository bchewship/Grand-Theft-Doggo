using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleScreen : MonoBehaviour
{
    private Button newGameButton;
    private Button continueButton;
    private Button exitButton;
    private GameManager gameManager;

    void Start()
    {
        newGameButton = GameObject.Find("NewGameButton").GetComponent<Button>();
        continueButton = GameObject.Find("ContinueButton").GetComponent<Button>();
        exitButton = GameObject.Find("ExitButton").GetComponent<Button>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        //starts new game
        newGameButton.onClick.AddListener(NewGame);
        //continueButton.onClick.AddListener(ContinueGame);
        //quits -- does nothing in the editor
        exitButton.onClick.AddListener(ExitGame);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void NewGame()
    {
        gameManager.StartGame();
    }

    void ContinueGame()
    {
        //continue for data persistence here
    }

    void ExitGame()
    {
        Application.Quit();
    }
}
