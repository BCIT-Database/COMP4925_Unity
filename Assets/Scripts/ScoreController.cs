using UnityEngine;
using TMPro;

public class ScoreController : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    //public GameObject gameOverPanel;
    private int score = 0;

    public delegate void ScoreChangedEvent(int newScore); 
    public event ScoreChangedEvent OnScoreChanged; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateScoreText();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddScore(int value)
    {
        score += value; 
        UpdateScoreText(); 
        OnScoreChanged?.Invoke(score); 
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

    public void ResetScore()
    {
        score = 0; 
        UpdateScoreText();
    }

    private void GameOver()
    {
        Debug.Log("Game Over!");
        //gameOverPanel.SetActive(true); 
        Time.timeScale = 0; 
    }
}
