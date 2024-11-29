using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1.0f;
    [SerializeField] private float jumpForce = 5.0f;
    [SerializeField] private LayerMask groundLayer;
    
    private Rigidbody2D rb;
    Animator animator;
    private bool isGrounded;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
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
        Jump();
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

    private void Jump()
    {

        bool jump = Input.GetButtonDown("Jump");
        if (jump && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce); 

            float horizontal = Input.GetAxis("Horizontal");
            if (horizontal > 0)
            {
                animator.SetBool("IsJumpingRight", true);
                animator.SetBool("IsJumpingLeft", false);
            }
            else if (horizontal < 0)
            {
                animator.SetBool("IsJumpingRight", false);
                animator.SetBool("IsJumpingLeft", true);
            }
            else
            {
               
                animator.SetBool("IsJumpingRight", true);
                animator.SetBool("IsJumpingLeft", false);
            }
        }
        else
        {
            
            animator.SetBool("IsJumpingRight", false);
            animator.SetBool("IsJumpingLeft", false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
      
        if (((1 << collision.gameObject.layer) & groundLayer) != 0)
        {
            isGrounded = true; 
            //Debug.Log("Player is on the ground");
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        
        if (((1 << collision.gameObject.layer) & groundLayer) != 0)
        {
            isGrounded = false; 
            //Debug.Log("Player left the ground");
        }
    }


}
