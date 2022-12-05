using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleScreen : MonoBehaviour
{
    //new game button moved to NewGame.cs
    private Button continueButton;
    private Button exitButton;
    private GameManager gameManager;

    void Start()
    {
        continueButton = GameObject.Find("ContinueButton").GetComponent<Button>();
        exitButton = GameObject.Find("ExitButton").GetComponent<Button>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        continueButton.onClick.AddListener(ContinueGame);
        //quits -- does nothing in the editor
        exitButton.onClick.AddListener(ExitGame);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ContinueGame()
    {
        gameManager.Load();
    }

    void ExitGame()
    {
        Application.Quit();
    }
}
