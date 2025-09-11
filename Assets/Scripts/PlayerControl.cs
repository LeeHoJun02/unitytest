using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private float currentSpeed;
    private float sprintSpeed = 10.0f;
    private float speed = 5.0f;
    private float jumpForce = 5.0f;
    private float moveInput;

    private bool isGrounded;
    private bool isJumping;
    private bool isSprinting;

    private Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
    }

    void FixedUpdate()
    {
        ApplyMovement();
    }

    void OnCollisionEnter2D(Collision2D collsion)
    {
        if(collsion.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D collsion)
    {
        if(collsion.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    void HandleInput()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
        isSprinting = Input.GetKey(KeyCode.LeftShift);
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            isJumping = true;
        }
    }

    void ApplyMovement()
    {
        rb.linearVelocity = new Vector2(moveInput * currentSpeed, rb.linearVelocity.y);
        if (isJumping)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isJumping = false;
        }
        currentSpeed = (isSprinting) ? sprintSpeed : speed;
    }
}
