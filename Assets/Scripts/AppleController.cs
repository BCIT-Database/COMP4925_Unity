using UnityEngine;
using UnityEngine.SceneManagement;

public class AppleController : MonoBehaviour
{
    [SerializeField] private int scoreValue = 10;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // When collide with Player
        {
            Object.FindFirstObjectByType<ScoreController>().AddScore(scoreValue);  // Add score
            Destroy(gameObject); // Remove apple 
        }
    }
}
