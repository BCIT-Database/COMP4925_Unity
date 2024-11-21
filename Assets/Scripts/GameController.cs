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
