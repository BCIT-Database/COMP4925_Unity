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
            levelController.LevelCompleted += OnLevelComplete; // 레벨 완료 이벤트 구독
            levelController.StartLevel(1); // 첫 번째 레벨 시작
        }
        else
        {
            Debug.LogError("LevelController not found on GameManager!");
        }

        if (scoreController == null)
        {
            Debug.LogError("ScoreController not found on GameManager!");
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

    private void EndGame()
    {
        Debug.Log("Game Over! Showing Game Over Scene.");
        SceneManager.LoadScene("Game Ove rScene"); 
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
