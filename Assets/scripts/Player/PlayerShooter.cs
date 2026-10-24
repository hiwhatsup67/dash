using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private Transform aimPivot;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float shotsPerSecond = 3f;
    [SerializeField] private AudioClip shootClip;

    private Camera mainCamera;
    private Collider2D ownCollider;
    private Vector2 aimDirection = Vector2.right;
    private float nextShotTime;
    private float fireRateMultiplier = 1f;

    private void Awake()
    {
        mainCamera = Camera.main;
        ownCollider = GetComponent<Collider2D>();
    }

    private void Update()
    {
        UpdateAim();

        if (Input.GetMouseButton(0) && Time.time >= nextShotTime)
        {
            Shoot();
        }
    }

    private void UpdateAim()
    {
        Vector2 mouseWorld = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        aimDirection = (mouseWorld - (Vector2)transform.position).normalized;

        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        aimPivot.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    private void Shoot()
    {
        Projectile projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        projectile.Launch(aimDirection, ownCollider);

        nextShotTime = Time.time + 1f / (shotsPerSecond * fireRateMultiplier);
        AudioPlayer.Play(shootClip);
    }

    public void MultiplyFireRate(float factor)
    {
        fireRateMultiplier *= factor;
    }
}
