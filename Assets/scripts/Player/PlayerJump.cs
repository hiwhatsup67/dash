using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(PlayerMovement))]
public class PlayerJump : MonoBehaviour
{
    private const string WallLayerName = "Wall";

    [SerializeField] private Transform visual;
    [SerializeField] private int maxCharges = 3;
    [SerializeField] private float secondsPerCharge = 3f;
    [SerializeField] private float jumpDistance = 2f;
    [SerializeField] private float jumpDurationSeconds = 0.25f;
    [SerializeField] private float visualJumpHeight = 0.6f;
    [SerializeField] private float landingCheckRadius = 0.45f;
    [SerializeField] private float landingSlideTimeoutSeconds = 0.5f;
    [SerializeField] private AudioClip jumpClip;

    private Rigidbody2D body;
    private PlayerMovement movement;
    private int playerLayer;
    private int wallLayer;
    private int wallMask;
    private int charges;
    private float rechargeTimer;
    private bool isGrounded = true;
    private Vector3 visualRestPosition;
    private Vector3 visualRestScale;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        movement = GetComponent<PlayerMovement>();

        playerLayer = gameObject.layer;
        wallLayer = LayerMask.NameToLayer(WallLayerName);
        wallMask = LayerMask.GetMask(WallLayerName);

        visualRestPosition = visual.localPosition;
        visualRestScale = visual.localScale;
        charges = maxCharges;
    }

    private void OnDisable()
    {
        Physics2D.IgnoreLayerCollision(playerLayer, wallLayer, false);
    }

    private void Update()
    {
        RechargeCharges();

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && charges > 0)
        {
            StartCoroutine(JumpRoutine(movement.FacingDirection));
        }
    }

    private void RechargeCharges()
    {
        if (charges >= maxCharges)
        {
            rechargeTimer = 0f;
            return;
        }

        rechargeTimer += Time.deltaTime;
        if (rechargeTimer < secondsPerCharge) return;

        rechargeTimer = 0f;
        charges++;
    }

    private IEnumerator JumpRoutine(Vector2 direction)
    {
        isGrounded = false;
        charges--;
        AudioPlayer.Play(jumpClip);

        Vector2 startPosition = body.position;
        movement.ControlLocked = true;
        Physics2D.IgnoreLayerCollision(playerLayer, wallLayer, true);
        body.linearVelocity = direction.normalized * (jumpDistance / jumpDurationSeconds);

        float elapsed = 0f;
        while (elapsed < jumpDurationSeconds)
        {
            elapsed += Time.deltaTime;
            float height = Mathf.Sin(Mathf.Clamp01(elapsed / jumpDurationSeconds) * Mathf.PI);
            visual.localPosition = visualRestPosition + Vector3.up * (height * visualJumpHeight);
            visual.localScale = visualRestScale * (1f + 0.3f * height);
            yield return null;
        }

        float slideElapsed = 0f;
        while (IsInsideWall() && slideElapsed < landingSlideTimeoutSeconds)
        {
            slideElapsed += Time.deltaTime;
            yield return null;
        }

        if (IsInsideWall()) body.position = startPosition;

        Land();
    }

    private bool IsInsideWall()
    {
        return Physics2D.OverlapCircle(body.position, landingCheckRadius, wallMask) != null;
    }

    private void Land()
    {
        body.linearVelocity = Vector2.zero;
        visual.localPosition = visualRestPosition;
        visual.localScale = visualRestScale;
        Physics2D.IgnoreLayerCollision(playerLayer, wallLayer, false);
        movement.ControlLocked = false;
        isGrounded = true;
    }
}
