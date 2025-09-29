using UnityEngine;

public class EnemyProto : MonoBehaviour
{
    private float moveSpeed = 3.0f;
    private float patrolRange = 2.0f;
    private float chaseRange = 5.0f;
    private int dir = 1;

    enum EnemyState
    {
        Patrolling,
        Chasing,
        Attacking
    }

    private EnemyState currentState;

    private Transform target;

    private Vector2 startPos;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        currentState = EnemyState.Patrolling;
        startPos = rb.position;

    }

    void FixedUpdate()
    {
        if (currentState == EnemyState.Patrolling)
        {
            Patrol();
        }
        else if (currentState == EnemyState.Chasing)
        {
            Chase();
        }
    }

    private void Patrol()
    {
        Vector2 currentPos = rb.position;
        rb.linearVelocity = new Vector2(dir * moveSpeed, rb.linearVelocity.y);

        float dx = currentPos.x - startPos.x;
        if (dx >= patrolRange)
        {
            dir = -1;
            rb.linearVelocity = new Vector2(dir * moveSpeed, rb.linearVelocity.y);
        }
        else if (dx <= -patrolRange)
        {
            dir = 1;
        }
    }

    private void Chase()
    {
        Vector2 currentPos = rb.position;
        float direction = Vector2.Distance(rb.position, target.position);


        if (direction <= chaseRange)
        {
            currentState = EnemyState.Chasing;
            rb.linearVelocity = new Vector2(dir * moveSpeed, rb.linearVelocity.y);

        }
    }
}