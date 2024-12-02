using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    public static GameController Instance
    {
        get
        {
            if (instance == null)
            {
                Debug.LogError("GameController instance is null!");
            }
            return instance;
        }
    }

    public bool isLoggedIn = false;
    public string username = "";
    public int currentLevel = 1;
    public int score = 0;

    private LevelController levelController;
    private ScoreController scoreController;

    public int targetScore = 30;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Keep the instance persistent
        }
        else
        {
            Destroy(gameObject); // Prevent duplicate instances
        }
    }

    private void Start()
    {
        levelController = GetComponent<LevelController>();
        scoreController = GetComponent<ScoreController>();

        if (levelController != null)
        {
            levelController.LevelCompleted += OnLevelComplete;
            levelController.GameOver += OnGameOver;
            levelController.StartLevel(1); 
        }
        else
        {
            Debug.LogError("LevelController not found on GameManager!");
        }

        if (scoreController != null)
        {
            scoreController.OnScoreChanged += HandleScoreChanged; 
        }
        else
        {
            Debug.LogError("ScoreController not found!");
        }
    }

    private void StartGame()
    {
        Debug.Log("Game started!");
        levelController.StartLevel(1); 
    }

    private void OnLevelComplete(int level)
    {
        Debug.Log($"Level {level} completed in GameController!");

        if (level < levelController.levelTimeLimits.Length)
        {
            levelController.StartLevel(level + 1); 
        }
        else
        {
            Debug.Log("All levels completed!");
            EndGame();
        }
    }

    private void OnGameOver()
    {
        Debug.Log("Game Over! Returning to the Game Over screen.");
        EndGame();
    }

    private void HandleScoreChanged(int currentScore)
    {
        Debug.Log($"Current Score: {currentScore}");

        if (currentScore >= targetScore) 
        {
            levelController.CompleteLevel();
        }
        else if (currentScore < 0) 
        {
            EndGame();
        }
    }

    private void EndGame()
    {
        Debug.Log("Game Over! Showing Game Over Scene.");
        SceneManager.LoadScene("Game Over Scene"); 
    }


    public void Login(string username)
    {
        this.isLoggedIn = true;
        this.username = username;
        Debug.Log($"User {username} logged in successfully.");
    }

    public void Logout()
    {
        isLoggedIn = false;
        username = "";
        currentLevel = 1;
        score = 0;
        SceneManager.LoadScene("Login  Scene");
    }

    public void UpdateGameProgress(int newLevel, int newScore)
    {
        currentLevel = newLevel;
        score = newScore;
        Debug.Log($"Game progress updated: Level {newLevel}, Score {newScore}");
    }
    public void LoadScene(string sceneName)
    {
        if (string.IsNullOrEmpty(sceneName))
        {
            Debug.LogError("Scene name is null or empty!");
            return;
        }

        Debug.Log($"Loading scene: {sceneName}");
        SceneManager.LoadScene(sceneName);
    }

}
