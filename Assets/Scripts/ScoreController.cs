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
        // Register 씬에서는 점수 텍스트를 업데이트하지 않음
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "Register Scene")
        {
            Debug.Log("ScoreController: Register scene detected. Skipping score initialization.");
            return;
        }

        if (scoreText == null)
        {
            Debug.LogError("ScoreText is not assigned in the Inspector!");
            return;
        }

        UpdateScoreText();
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
