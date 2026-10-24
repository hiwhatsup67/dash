using System;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    public static ScoreKeeper Instance { get; private set; }

    public int Score { get; private set; }
    public int StarsCollected { get; private set; }

    public event Action Changed;

    private void Awake()
    {
        Instance = this;
    }

    public void AddScore(int points)
    {
        Score += points;
        Changed?.Invoke();
    }

    public void AddStar()
    {
        StarsCollected++;
        Changed?.Invoke();
    }
}
