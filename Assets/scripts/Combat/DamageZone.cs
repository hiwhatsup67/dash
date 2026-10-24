using UnityEngine;

public class DamageZone : MonoBehaviour
{
    [SerializeField] private int damage = 1;
    [SerializeField] private float secondsBetweenHits = 1f;
    [SerializeField] private LayerMask targetLayers;

    private float nextHitTime;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (Time.time < nextHitTime) return;
        if ((targetLayers.value & (1 << other.gameObject.layer)) == 0) return;

        Health health = other.GetComponentInParent<Health>();
        if (health == null) return;

        health.TakeDamage(damage);
        nextHitTime = Time.time + secondsBetweenHits;
    }
}
