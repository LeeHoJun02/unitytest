using UnityEngine;
using System.Collections;


public class PlayerControl : MonoBehaviour
{
    enum PlayerState
    {
        Idle,
        Walking,
        Running,
        Jumping,
        Falling,
        Attacking,
        UsingAbility1,
        UsingAbility2
    }


    private float currentHealth = 100.0f;
    private float maxHealth = 100.0f;
    private float currentStamina = 50.0f;
    private float maxStamina = 50.0f;

    private float currentSpeed;
    private float sprintSpeed = 10.0f;
    private float speed = 5.0f;
    private float jumpForce = 7.0f;
    private float moveInput;

    private PlayerState currentState;

    private bool isGrounded;
    private bool isJumping;
    private bool isSprinting;
    private bool isAttacking;

    private Coroutine staminaRoutine;

    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Animator anim;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        MainHandleInput();
        HandlePlayerState();
    }

    void FixedUpdate()
    {
        ApplyMovement();
    }

    void OnCollisionEnter2D(Collision2D collsion)
    {
        if (collsion.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;

        }
    }

    void OnCollisionExit2D(Collision2D collsion)
    {
        if (collsion.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    void HandlePlayerState()
    {
        // 요 부분은 gpt가 작성한 부분이여서 나중에 추가적으로 더 이해 및 생각
        if (!isGrounded)
        {
            float vy = rb.linearVelocity.y;
            if (vy > 0.1f)
            {
                currentState = PlayerState.Jumping;
                Debug.Log("Jumping");
                anim.SetTrigger("jump");
            }
            else if (vy < -0.1f)
            {
                currentState = PlayerState.Falling;
                Debug.Log("Falling");
                anim.SetTrigger("fall");
            }
            else currentState = PlayerState.Jumping;
        }
        else
        {
            if (isSprinting && Mathf.Abs(moveInput) > 0.01f && currentStamina > 0)
            {
                currentState = PlayerState.Running;
                Debug.Log("Running");
                anim.SetBool("run", true);
                anim.SetBool("move", false);
            }
            else if (Mathf.Abs(moveInput) > 0.01f)
            {
                currentState = PlayerState.Walking;
                Debug.Log("Walking");
                anim.SetBool("move", true);
                anim.SetBool("run", false);
            }
            else
            {
                currentState = PlayerState.Idle;
                anim.SetBool("move", false);
                anim.SetBool("run", false);


            }

        }
        //요기까지
        if (isAttacking)
        {
            Debug.Log("Attacking");
        }
    }

    //여기서부터 다시 공부해서 하기 이건 도저히 아직 이해가 안된다
    IEnumerator regenStamina()
    {
        yield return new WaitForSeconds(2);
        while (currentStamina < maxStamina)
        {
            currentStamina += 10 * Time.deltaTime;
            yield return null;
        }
    }

    void MainHandleInput()
    {
        HandlMoveInput();
        HandleAttackInput();
    }

    void HandlMoveInput()
    {

        moveInput = Input.GetAxisRaw("Horizontal");
        if (moveInput < 0)
        {
            sprite.flipX = true;
        }
        else if (moveInput > 0)
        {
            sprite.flipX = false;
        }
        isSprinting = Input.GetKey(KeyCode.LeftShift);
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            isJumping = true;
        }
    }

    void HandleAttackInput()
    {
        isAttacking = Input.GetMouseButtonDown(0);
        if (isAttacking)
        {
            Debug.Log("Attack");
            anim.SetTrigger("attack");
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Use Ability2");
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("Use Ability1");
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
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
        if (isSprinting && currentStamina > 0 && currentState == PlayerState.Running)
        {
            currentSpeed = sprintSpeed;
            currentStamina -= 5 * Time.deltaTime;
        }
        else
        {
            currentSpeed = speed;
        }
    }

    //gpt
    private void OnGUI()
    {
        GUI.Box(new Rect(10, 10, 100, 20), "Health: " + (int)currentHealth + "/" + (int)maxHealth);
        GUI.Box(new Rect(10, 40, 100, 20), "Stamina: " + (int)currentStamina + "/" + (int)maxStamina);
    }
}
