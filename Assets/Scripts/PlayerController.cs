using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float rightSpeed = 6f;
    public float leftSpeed = 2.5f;
    public Rigidbody2D rb;

    private float currentSpeed;
    
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            currentSpeed = rightSpeed;
        }
        else
        {
            currentSpeed = -leftSpeed;
        }
    }

    private void FixedUpdate()
    {
        Vector2 velocity = rb.linearVelocity;
        velocity.x = currentSpeed;
        rb.linearVelocity = velocity;
    }
}