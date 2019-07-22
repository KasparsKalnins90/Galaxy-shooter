using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    [SerializeField]
    public GameObject _player;
    public Sprite[] lives;
    public Image livesImageDisplay;
    public int score;
    public Text scoreText;
    public GameObject titleScreen;

    
    


    public void UpdateLives(int currentLives)
    {
        
        livesImageDisplay.sprite = lives[currentLives];
        
    }
    public void resetScore()
    {
        score = 0;
        scoreText.text = "Score: " + score;
    }
    public void updateScore()
    {
        score += 10;
        scoreText.text = "Score: " + score;
        
    }
    public void ShowTitleScreen()
    {
        titleScreen.SetActive(true);
    }
    public void HideTitleScreen()
    {
        titleScreen.SetActive(false);
    }
}


