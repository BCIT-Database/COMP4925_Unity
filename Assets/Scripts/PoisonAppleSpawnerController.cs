using UnityEngine;

public class PoisonAppleController : MonoBehaviour
{
    [SerializeField] private GameObject poisonApplePrefab; // 독사과 프리팹
    [SerializeField] private float spawnInterval = 2f; // 독사과 생성 간격
    [SerializeField] private Transform[] spawnPoints; // 독사과 생성 위치 배열
    [SerializeField] private int damageValue = 5; // 독사과 점수 감소 값

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
