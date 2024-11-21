using UnityEngine;
using UnityEngine.InputSystem.iOS;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour

{

    public void OnStartGameButtonClicked()
    {
        GameController.Instance.LoadScene("Game Scene");
    }

    public void OnLogoutButtonClicked()
    {
        GameController.Instance.Logout();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
