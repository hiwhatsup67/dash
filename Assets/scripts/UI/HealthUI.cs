using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private Health playerHealth;
    [SerializeField] private Text label;

    private void Start()
    {
        Refresh(playerHealth.Current, playerHealth.Max);
    }

    private void OnEnable()
    {
        playerHealth.Changed += Refresh;
    }

    private void OnDisable()
    {
        playerHealth.Changed -= Refresh;
    }

    private void Refresh(int current, int max)
    {
        label.text = $"HP: {current}/{max}";
    }
}
