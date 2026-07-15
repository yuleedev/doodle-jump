using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float rightSpeed = 6f;
    public float leftSpeed = 2.5f;
    public Rigidbody2D rb;
    public Camera cam;

    private SpriteRenderer sr;
    private float currentSpeed;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        if (cam == null) cam = Camera.main;
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

        Wrap();
    }

    void Wrap()
    {
        float halfWidth = cam.orthographicSize * cam.aspect;
        float halfSprite = sr.bounds.extents.x;

        Vector3 position = transform.position;

        if (position.x - halfSprite > halfWidth)
        {
            position.x = -halfWidth - halfSprite;
            transform.position = position;
        }
        else if (position.x + halfSprite < -halfWidth)
        {
            position.x = halfWidth + halfSprite;
            transform.position = position;
        }
    }

    private void FixedUpdate()
    {
        Vector2 velocity = rb.linearVelocity;
        velocity.x = currentSpeed;
        rb.linearVelocity = velocity;
    }
}