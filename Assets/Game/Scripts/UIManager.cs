using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Sprite[] lives;
    public Image livesImageDisplay;
    public Text scoreText;
    public GameObject titileScreen;
    public int Score;

    public void UpdateLives(int currentLives)
    {
      Debug.Log("Player lives "+ currentLives);
      livesImageDisplay.sprite = lives[currentLives];

    }

    public void UpdateScore()
    {
        
        Score +=10;
        scoreText.text = "Score :" + Score;
    }

    public void ShowTitleScreen()
    {
        titileScreen.SetActive(true);
    }

    public void HideTitleScreen()
    {
        titileScreen.SetActive(false);
        scoreText.text = "Score :";
    }
}
