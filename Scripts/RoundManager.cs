using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundManager : MonoBehaviour
{

    public int remainingMoves;
    private UIManager uiMan;
   

    private Board board;

    private MatchFinder matchFinder;

    private bool gameOver = false;

    public int currentScore;
    public string levelName;



    
    // Start is called before the first frame update
    void Awake()
    {
        uiMan = FindObjectOfType<UIManager>();
        board = FindObjectOfType<Board>();
       matchFinder = FindObjectOfType<MatchFinder>();
    }

    // Update is called once per frame
    void Update()
    {
        if (uiMan != null) {
        uiMan.remainingText.text = remainingMoves.ToString();
        uiMan.scoreText.text = currentScore.ToString();
        }

        
        
        if (remainingMoves <= 0 && !gameOver) {
            gameOver = true; // Prevent multiple game over checks
            StoreHighestScore();
            lossCheck();
            winCheck();
        } 

    }

    private void StoreHighestScore() {
       
        int highestScore = PlayerPrefs.GetInt(levelName + "HighScore", 0);

        
        if(currentScore > highestScore) {
            PlayerPrefs.SetInt(levelName + "HighScore", currentScore);
            PlayerPrefs.Save(); 
        }
    }


    private void lossCheck() {
        uiMan.roundOverScreen.SetActive(true);
    }

    private void winCheck() {
        Debug.Log("winCheck called");
    
    if (board.allGems != null && board.allGems.Length > 0) {
        if (!matchFinder.CheckPossibleMatch()) {
            Debug.Log("Game Over! You Win!");
            uiMan.winScreen.SetActive(true);
        }
    }
}
}
