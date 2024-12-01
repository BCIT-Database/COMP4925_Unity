using TMPro;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public TextMeshProUGUI timerText; 
    public float[] levelTimeLimits = { 20f, 10f, 5f };

    private int currentLevel = 1;
    private float remainingTime; 
    private bool isTimerActive = false;

    public delegate void LevelCompletedEvent(int level); 
    public event LevelCompletedEvent LevelCompleted; 

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
            CompleteLevel(); 
        }
    }

    private void UpdateTimerUI()
    {
        if (timerText != null)
        {
            int seconds = Mathf.FloorToInt(remainingTime % 60);
            timerText.text = $"Remaininig Time: {seconds:00} Seconds";
        }
    }

    private void CompleteLevel() 
    {
        Debug.Log($"Level {currentLevel} completed!");
        LevelCompleted?.Invoke(currentLevel); 
    }
}
