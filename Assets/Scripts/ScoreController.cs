using UnityEngine;
using TMPro;

public class ScoreController : MonoBehaviour
{
    public TextMeshProUGUI scoreText; 
    private int score = 0; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateScoreText();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddScore(int value)
    {
        score += value; // increase the score
        UpdateScoreText(); // Update the text
    }

    private void UpdateScoreText()
    {
        scoreText.text = "Score: " + score; // display the score
    }
}
