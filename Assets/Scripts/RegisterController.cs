using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RegisterController : MonoBehaviour
{
    [SerializeField] TMP_InputField emailInput;
    [SerializeField] TMP_InputField usernameInput;
    [SerializeField] TMP_InputField passwordInput;
    [SerializeField] TMP_InputField confirmPasswordInput;
    [SerializeField] TMP_Text errorMessage;
    [SerializeField] Button registerButton;

    private bool isProcessing = false;

    private void Start()
    {
        if (registerButton == null || emailInput == null || usernameInput == null || passwordInput == null || confirmPasswordInput == null)
        {
            Debug.LogError("RegisterController UI elements are not assigned in the Inspector.");
            return;
        }

        registerButton.onClick.AddListener(OnRegisterClicked);
    }

    public void OnRegisterClicked()
    {
        if (isProcessing) return;

        isProcessing = true;

        string email = emailInput.text.Trim();
        string username = usernameInput.text.Trim();
        string password = passwordInput.text;
        string confirmPassword = confirmPasswordInput.text;

        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            DisplayError("All fields are required.");
            isProcessing = false;
            return;
        }

        if (!IsEmailValid(email))
        {
            DisplayError("Invalid email format!");
            isProcessing = false;
            return;
        }

        if (!IsPasswordStrong(password))
        {
            DisplayError("Password must include uppercase, lowercase, number, and special character.");
            isProcessing = false;
            return;
        }

        if (password != confirmPassword)
        {
            DisplayError("Passwords do not match!");
            isProcessing = false;
            return;
        }

        errorMessage.text = ""; // Clear any previous error messages
        StartCoroutine(RegisterUser(email, username, password));
    }

    private IEnumerator RegisterUser(string email, string username, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("email", email);
        form.AddField("username", username);
        form.AddField("password", password);

        using (UnityWebRequest request = UnityWebRequest.Post(APIConfig.REGISTER_ENDPOINT, form))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("User registered successfully!");
                GameController.Instance.Login(username);
                SceneManager.LoadScene("Login  Scene");
            }
            else
            {
                DisplayError($"Registration failed: {request.error}");
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

    private bool IsEmailValid(string email)
    {
        return System.Text.RegularExpressions.Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
    }

    private bool IsPasswordStrong(string password)
    {
        if (password.Length < 8) return false;
        if (!System.Text.RegularExpressions.Regex.IsMatch(password, @"[A-Z]")) return false;
        if (!System.Text.RegularExpressions.Regex.IsMatch(password, @"[a-z]")) return false;
        if (!System.Text.RegularExpressions.Regex.IsMatch(password, @"[0-9]")) return false;
        if (!System.Text.RegularExpressions.Regex.IsMatch(password, @"[\W_]")) return false;
        return true;
    }
}
