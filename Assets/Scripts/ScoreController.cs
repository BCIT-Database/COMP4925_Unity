using UnityEngine;
using TMPro;

public class ScoreController : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    //public GameObject gameOverPanel;
    private int score = 0; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateScoreText();
        //gameOverPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddScore(int value)
    {
        score += value; // increase the score
        UpdateScoreText(); // Update the text
    }

    public void SubtractScore(int value)
    {
        score -= value; 
        UpdateScoreText();

        if (score < 0) 
        {
            GameOver();
        }
    }

    private void UpdateScoreText()
    {
        scoreText.text = "Score: " + score; // display the score
    }

    private void GameOver()
    {
        Debug.Log("Game Over!");
        //gameOverPanel.SetActive(true); 
        Time.timeScale = 0; 
    }
}
