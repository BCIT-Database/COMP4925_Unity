using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginController : MonoBehaviour
{
    [SerializeField] TMP_InputField usernameInput;
    [SerializeField] TMP_InputField passwordInput;
    [SerializeField] TMP_Text errorMessage;
    [SerializeField] Button loginButton;

    private bool isProcessing = false;

    private void Start()
    {
        if (loginButton == null || usernameInput == null || passwordInput == null)
        {
            Debug.LogError("LoginController UI elements are not assigned in the Inspector.");
            return;
        }

        loginButton.onClick.AddListener(OnLoginClicked);
    }

    public void OnLoginClicked()
    {
        if (isProcessing) return;

        isProcessing = true;

        string username = usernameInput.text.Trim();
        string password = passwordInput.text;

        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            DisplayError("Username and password are required.");
            isProcessing = false;
            return;
        }

        errorMessage.text = ""; // Clear previous errors
        StartCoroutine(LoginUser(username, password));
    }

    private IEnumerator LoginUser(string username, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("password", password);

        using (UnityWebRequest request = UnityWebRequest.Post(APIConfig.LOGIN_ENDPOINT, form))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("User logged in successfully!");
                GameController.Instance.Login(username);
                SceneManager.LoadScene("Start Scene"); // Replace with your game scene name
            }
            else
            {
                DisplayError($"Login failed: {request.error}");
            }
        }

        isProcessing = false;
    }

    private void DisplayError(string message)
    {
        errorMessage.text = message;
        Debug.LogError(message);
        isProcessing = false;
    }
}
