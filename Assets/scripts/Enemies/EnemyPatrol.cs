using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer))]
public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] private float speed = 1.5f;
    [SerializeField] private float patrolRadius = 2f;

    private Rigidbody2D body;
    private SpriteRenderer spriteRenderer;
    private float leftEdge;
    private float rightEdge;
    private float direction = 1f;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        leftEdge = body.position.x - patrolRadius;
        rightEdge = body.position.x + patrolRadius;
    }

    private void FixedUpdate()
    {
        if (body.position.x > rightEdge) direction = -1f;
        else if (body.position.x < leftEdge) direction = 1f;

        body.linearVelocity = new Vector2(direction * speed, body.linearVelocity.y);
        spriteRenderer.flipX = direction < 0f;
    }
}
