using UnityEngine;

public class BulletClearAbility : MonoBehaviour
{
    private const string BulletLayerName = "Bullet";

    [SerializeField] private float radius = 3f;
    [SerializeField] private AudioClip activateClip;

    private int bulletMask;
    private bool isAvailable = true;

    private void Awake()
    {
        bulletMask = LayerMask.GetMask(BulletLayerName);
    }

    private void Update()
    {
        if (isAvailable && Input.GetKeyDown(KeyCode.F))
        {
            Activate();
        }
    }

    private void Activate()
    {
        isAvailable = false;
        AudioPlayer.Play(activateClip);

        foreach (Collider2D hit in Physics2D.OverlapCircleAll(transform.position, radius, bulletMask))
        {
            if (hit.TryGetComponent(out Projectile projectile))
            {
                Destroy(projectile.gameObject);
            }
        }
    }

    public void Recharge()
    {
        isAvailable = true;
    }
}
