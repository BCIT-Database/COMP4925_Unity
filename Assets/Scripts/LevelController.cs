using TMPro;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public GameObject LevelCompletePanel;
    public float[] levelTimeLimits = { 20f, 10f, 5f };

    private int currentLevel = 1;
    private float remainingTime; 
    private bool isTimerActive = false;

    public delegate void LevelCompletedEvent(int level); 
    public event LevelCompletedEvent LevelCompleted;

    public delegate void GameOverEvent();
    public event GameOverEvent GameOver;

    private void Update()
    {
        if (isTimerActive)
        {
            UpdateTimer();
        }
    }

    public void StartLevel(int level)
    {
        if (level > 0 && level <= levelTimeLimits.Length)
        {
            currentLevel = level;
            remainingTime = levelTimeLimits[level - 1];
            isTimerActive = true;
            LevelCompletePanel.SetActive(false);

            Debug.Log($"Level {level} started with {remainingTime} seconds.");
        }
        else
        {
            Debug.LogError("Invalid level number!");
        }
    }

    private void UpdateTimer()
    {
        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
            UpdateTimerUI();
        }
        else
        {
            remainingTime = 0;
            isTimerActive = false;
            HandleGameOver(); ; 
        }
    }

    private void UpdateTimerUI()
    {
        if (timerText != null)
        {
            int seconds = Mathf.FloorToInt(remainingTime % 60);
            timerText.text = $"Remaininig Time: {seconds:00}";
        }
    }

    private void HandleGameOver()
    {
        Debug.Log("Time's up! Game Over!");
        GameOver?.Invoke();
    }

    public void CompleteLevel() 
    {
        Debug.Log($"Level {currentLevel} completed!");
        LevelCompletePanel.SetActive(true);
        LevelCompleted?.Invoke(currentLevel); 
    }
    public void NextLevel()
    {
        if (currentLevel < levelTimeLimits.Length)
        {
            FindObjectOfType<ScoreController>().ResetScore();
            StartLevel(currentLevel + 1); 
        }
        else
        {
            Debug.Log("All levels completed!");
         
        }
    }

    public int GetCurrentLevel()
    {
        return currentLevel;
    }
}
