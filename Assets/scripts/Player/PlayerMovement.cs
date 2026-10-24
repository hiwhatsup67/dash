using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float walkSpeed = 2f;
    [SerializeField] private float runMultiplier = 3f;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private Rigidbody2D body;
    private Vector2 moveInput;
    private bool isRunning;
    private float speedUpgradeMultiplier = 1f;

    public Vector2 FacingDirection { get; private set; } = Vector2.right;
    public bool ControlLocked { get; set; }

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        isRunning = Input.GetKey(KeyCode.LeftShift);

        if (moveInput != Vector2.zero) FacingDirection = moveInput;
        if (moveInput.x != 0) spriteRenderer.flipX = moveInput.x < 0;
    }

    private void FixedUpdate()
    {
        if (ControlLocked) return;

        float speed = walkSpeed * speedUpgradeMultiplier * (isRunning ? runMultiplier : 1f);
        body.linearVelocity = moveInput * speed;
    }

    public void MultiplySpeed(float factor)
    {
        speedUpgradeMultiplier *= factor;
    }
}
