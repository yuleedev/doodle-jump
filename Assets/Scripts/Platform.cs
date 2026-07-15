using UnityEngine;

public class Platform : MonoBehaviour
{
    public float jumpForce = 12f;
    public float springForce = 26f;
    public GameObject spring;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.relativeVelocity.y <= 0f)
        {
            Rigidbody2D rb = collision.collider.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                float force = jumpForce;
                if (spring != null && spring.activeSelf && HitSpring(collision.collider))
                {
                    force = springForce;
                }

                Vector2 velocity = rb.linearVelocity;
                velocity.y = force;
                rb.linearVelocity = velocity;
            }
        }
    }

    bool HitSpring(Collider2D playerCollider)
    {
        SpriteRenderer sr = spring.GetComponent<SpriteRenderer>();
        float dx = Mathf.Abs(playerCollider.bounds.center.x - sr.bounds.center.x);
        return dx < sr.bounds.extents.x + playerCollider.bounds.extents.x;
    }
}