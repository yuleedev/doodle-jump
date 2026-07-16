using UnityEngine;

public class Platform : MonoBehaviour
{
    public float jumpForce = 12f;
    public float springForce = 26f;
    public GameObject spring;
    public AudioClip springSound;

    public Sprite normalSprite;
    public Sprite breakableSprite;
    public Sprite brokenSprite;
    public AudioClip breakSound;
    public float breakFallSpeed = 8f;

    private bool isBreakable = false;
    private bool hasBroken = false;

    private AudioSource audioSource;
    private SpriteRenderer sr;
    private Collider2D col;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        sr = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
    }

    void Update()
    {
        if (hasBroken)
        {
            transform.position += Vector3.down * breakFallSpeed * Time.deltaTime;
        }
    }

    public void SetBreakable(bool value)
    {
        isBreakable = value;
        hasBroken = false;

        if (col != null) col.enabled = true;

        if (sr != null)
        {
            if (value && breakableSprite != null) sr.sprite = breakableSprite;
            else if (!value && normalSprite != null) sr.sprite = normalSprite;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.relativeVelocity.y > 0f) return;

        Rigidbody2D rb = collision.collider.GetComponent<Rigidbody2D>();
        if (rb == null) return;

        if (isBreakable)
        {
            Break(rb, collision.relativeVelocity.y);
            return;
        }

        float force = jumpForce;

        if (spring != null && spring.activeSelf && HitSpring(collision.collider))
        {
            force = springForce;

            if (audioSource != null && springSound != null)
            {
                audioSource.PlayOneShot(springSound);
            }
        }

        Vector2 velocity = rb.linearVelocity;
        velocity.y = force;
        rb.linearVelocity = velocity;
    }

    void Break(Rigidbody2D rb, float incomingVelocityY)
    {
        if (hasBroken) return;

        hasBroken = true;

        if (col != null) col.enabled = false;
        if (sr != null && brokenSprite != null) sr.sprite = brokenSprite;

        if (audioSource != null && breakSound != null)
        {
            audioSource.PlayOneShot(breakSound);
        }

        Vector2 velocity = rb.linearVelocity;
        velocity.y = incomingVelocityY;
        rb.linearVelocity = velocity;
    }

    bool HitSpring(Collider2D playerCollider)
    {
        SpriteRenderer springSr = spring.GetComponent<SpriteRenderer>();
        float dx = Mathf.Abs(playerCollider.bounds.center.x - springSr.bounds.center.x);
        return dx < springSr.bounds.extents.x + playerCollider.bounds.extents.x;
    }
}