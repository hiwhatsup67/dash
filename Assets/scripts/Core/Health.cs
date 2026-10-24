using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth = 3;

    private int currentHealth;

    public int Current => currentHealth;
    public int Max => maxHealth;

    public event Action<int, int> Changed;
    public event Action Died;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        if (currentHealth <= 0) return;

        currentHealth = Mathf.Max(0, currentHealth - amount);
        Changed?.Invoke(currentHealth, maxHealth);

        if (currentHealth == 0) Died?.Invoke();
    }

    public void AddMaxHealth(int amount)
    {
        maxHealth += amount;
        currentHealth += amount;
        Changed?.Invoke(currentHealth, maxHealth);
    }
}
