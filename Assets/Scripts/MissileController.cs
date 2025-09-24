using UnityEngine;

public class MissileController : MonoBehaviour
{
    private float speed = 10f;
    private float lifetime = 5f;
    private Rigidbody2D rb;
    private Transform[] waypoints;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        rb.linearVelocity = transform.right * speed;
        Invoke(nameof(DestroyMissile), lifetime);
    }

    void DestroyMissile()
    {
        Destroy(gameObject);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
