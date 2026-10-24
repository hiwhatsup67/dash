using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private float secondsBetweenShots = 2f;
    [SerializeField] private float muzzleDistance = 0.8f;
    [SerializeField] private AudioClip shootClip;

    private Transform target;
    private Collider2D ownCollider;
    private int sightMask;
    private float nextShotTime;

    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        target = player != null ? player.transform : null;

        ownCollider = GetComponent<Collider2D>();
        sightMask = LayerMask.GetMask("Ground", "Player");
        nextShotTime = Time.time + secondsBetweenShots;
    }

    private void Update()
    {
        if (target == null || !target.gameObject.activeInHierarchy) return;
        if (Time.time < nextShotTime || !HasLineOfSight()) return;

        Shoot();
    }

    private void Shoot()
    {
        Vector2 direction = (target.position - transform.position).normalized;
        Vector2 spawnPosition = (Vector2)transform.position + direction * muzzleDistance;

        Projectile projectile = Instantiate(projectilePrefab, spawnPosition, Quaternion.identity);
        projectile.Launch(direction, ownCollider);

        AudioPlayer.Play(shootClip);
        nextShotTime = Time.time + secondsBetweenShots;
    }

    private bool HasLineOfSight()
    {
        Vector2 toTarget = target.position - transform.position;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, toTarget, toTarget.magnitude, sightMask);
        return hit.collider != null && hit.collider.CompareTag("Player");
    }
}
