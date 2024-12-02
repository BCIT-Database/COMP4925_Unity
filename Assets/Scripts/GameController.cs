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

    public LevelController levelController; 
    public ScoreController scoreController;

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
        // 현재 씬이 Register 또는 Login인 경우 게임 관련 초기화 생략
        string currentScene = SceneManager.GetActiveScene().name;
        if (currentScene == "Register Scene" || currentScene == "Login Scene")
        {
            Debug.Log("Register or Login Scene detected. Skipping game-related initialization.");
            return;
        }

        // 게임 관련 초기화
        InitializeGame();
    }

    private void InitializeGame()
    {
        // LevelController 초기화
        if (levelController == null)
        {
            Debug.LogError("LevelController is not assigned in the Inspector!");
        }
        else
        {
            levelController.LevelCompleted += OnLevelComplete;
            levelController.GameOver += OnGameOver;
            levelController.StartLevel(1);
        }

        // ScoreController 초기화
        if (scoreController == null)
        {
            Debug.LogError("ScoreController is not assigned in the Inspector!");
        }
        else
        {
            scoreController.OnScoreChanged += HandleScoreChanged;
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
        SceneManager.LoadScene("Login Scene");
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
