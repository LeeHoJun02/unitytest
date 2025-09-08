using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private float currentSpeed;
    private float sprintSpeed = 10.0f;
    private float speed = 5.0f;
    private float jumpForce = 5.0f;
    private float moveInput;

    private bool isGrounded;

    private Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(moveInput * currentSpeed, rb.linearVelocity.y);

        if(Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed = sprintSpeed;
        }
        else
        {
            currentSpeed = speed;
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isGrounded = false;
        }
    }

    void OnCollisionEnter2D(Collision2D collsion)
    {
        if(collsion.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}
