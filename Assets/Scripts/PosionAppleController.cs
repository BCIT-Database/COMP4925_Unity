using UnityEngine;
using UnityEngine.SceneManagement;

public class PoisonAppleManager : MonoBehaviour
{
    [SerializeField] private GameObject poisonApplePrefab; 
    [SerializeField] private float spawnInterval = 2f; 
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private int damageValue = 5; 

    void Start()
    {
        InvokeRepeating(nameof(SpawnPoisonApple), 1f, spawnInterval); 
    }

    private void SpawnPoisonApple()
    {
       
        int randomIndex = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[randomIndex];

        
        GameObject poisonApple = Instantiate(poisonApplePrefab, spawnPoint.position, Quaternion.identity);

        
        poisonApple.AddComponent<PoisonAppleComponent>().Initialize(damageValue);
    }
}

public class PoisonAppleComponent : MonoBehaviour
{
    private int damageValue; 

    public void Initialize(int damage)
    {
        damageValue = damage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            FindObjectOfType<ScoreController>().SubtractScore(damageValue);
            Destroy(gameObject); 
        }
        else if (collision.CompareTag("Finish")) 
        {
            Destroy(gameObject); 
        }
    }
}