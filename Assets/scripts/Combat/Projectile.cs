using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 6f;
    [SerializeField] private float lifetimeSeconds = 3f;
    [SerializeField] private int damage = 1;
    [SerializeField] private float ownerGraceSeconds = 0.2f;

    private Rigidbody2D body;
    private Collider2D ownCollider;
    private Collider2D ownerCollider;
    private Vector2 velocityBeforeImpact;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        ownCollider = GetComponent<Collider2D>();
    }

    public void Launch(Vector2 direction, Collider2D owner)
    {
        body.linearVelocity = direction.normalized * speed;
        velocityBeforeImpact = body.linearVelocity;

        ownerCollider = owner;
        if (ownerCollider != null)
        {
            Physics2D.IgnoreCollision(ownCollider, ownerCollider, true);
            Invoke(nameof(RestoreOwnerCollision), ownerGraceSeconds);
        }

        Destroy(gameObject, lifetimeSeconds);
    }

    private void FixedUpdate()
    {
        // Physics already changed our velocity by the time a collision is reported, so keep the previous one.
        velocityBeforeImpact = body.linearVelocity;
    }

    private void RestoreOwnerCollision()
    {
        if (ownerCollider != null) Physics2D.IgnoreCollision(ownCollider, ownerCollider, false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Health health = collision.collider.GetComponentInParent<Health>();
        if (health != null)
        {
            health.TakeDamage(damage);
            Destroy(gameObject);
            return;
        }

        Vector2 normal = collision.GetContact(0).normal;
        body.linearVelocity = Vector2.Reflect(velocityBeforeImpact, normal).normalized * speed;
    }
}
