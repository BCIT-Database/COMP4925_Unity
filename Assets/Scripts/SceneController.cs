using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour

{

    public void OnStartGameButtonClicked()
    {
        SceneManager.LoadScene("Game Scene");
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
