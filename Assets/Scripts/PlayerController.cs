using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1.0f;

    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {

        
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        transform.Translate(moveSpeed * horizontal * Time.deltaTime, moveSpeed * vertical * Time.deltaTime, 0);

        bool fire1 = Input.GetButtonDown("Fire1");
        if (fire1)
        {
            Debug.Log("Pressed: " + fire1);
        }

        //Input.GetKey(KeyCode.);
        animator.SetBool("IsWalkingStraight", horizontal > 0);
        animator.SetBool("IsWalkingBack", horizontal < 0);



    }


}
