using UnityEngine;

public enum StarEffect
{
    MaxHealth,
    Speed,
    FireRate,
    RechargeBulletClear
}

public class Star : MonoBehaviour
{
    [SerializeField] private StarEffect effect;
    [SerializeField] private float multiplier = 1.5f;
    [SerializeField] private int healthBonus = 1;
    [SerializeField] private int pointsOnPickup = 10;
    [SerializeField] private AudioClip pickupClip;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        ApplyEffect(other);
        ScoreKeeper.Instance.AddStar();
        ScoreKeeper.Instance.AddScore(pointsOnPickup);
        AudioPlayer.Play(pickupClip);
        Destroy(gameObject);
    }

    private void ApplyEffect(Collider2D player)
    {
        switch (effect)
        {
            case StarEffect.MaxHealth:
                Health health = player.GetComponentInParent<Health>();
                if (health != null) health.AddMaxHealth(healthBonus);
                break;
            case StarEffect.Speed:
                PlayerMovement movement = player.GetComponentInParent<PlayerMovement>();
                if (movement != null) movement.MultiplySpeed(multiplier);
                break;
            case StarEffect.FireRate:
                PlayerShooter shooter = player.GetComponentInParent<PlayerShooter>();
                if (shooter != null) shooter.MultiplyFireRate(multiplier);
                break;
            case StarEffect.RechargeBulletClear:
                BulletClearAbility bulletClear = player.GetComponentInParent<BulletClearAbility>();
                if (bulletClear != null) bulletClear.Recharge();
                break;
        }
    }
}
