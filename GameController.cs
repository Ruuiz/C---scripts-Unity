using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;


public class GameController : MonoBehaviour
{

    public GameObject gameOver;
    public static GameController instance;
    public int totalScore = 0;
    public Text scoreText;

    public GameObject gameComplete;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        totalScore = PlayerPrefs.GetInt("score");
        scoreText.text = totalScore.ToString();
        PlayerPrefs.SetInt("score", 0);
    }

    public void ShowGameOver()
    {
        gameOver.SetActive(true);
        PlayerPrefs.SetInt("score", 0);
    }

    public void RestartGame(string lvlName)
    {
        SceneManager.LoadScene(lvlName);
    }

    public void UpdateScoreText()
    {
        scoreText.text = totalScore.ToString();

        PlayerPrefs.SetInt("score", totalScore);      
    }

    public void ShowGameComplete()
    {
        gameComplete.SetActive(true);
    }
}
