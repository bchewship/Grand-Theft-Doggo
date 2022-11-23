using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewGame : MonoBehaviour
{
    //moved all this from TitleScreen.cs because it would give the player 3 random
    //characters rather than 1, because the script was attached to 3 buttons

    private Button newGameButton;
    private GameManager gameManager;

    void Start()
    {
        newGameButton = GameObject.Find("NewGameButton").GetComponent<Button>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        //starts new game
        newGameButton.onClick.AddListener(StartNewGame);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void StartNewGame()
    {
        gameManager.StartGame();
    }
}
