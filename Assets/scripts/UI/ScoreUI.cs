using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private Text label;

    private void Start()
    {
        ScoreKeeper.Instance.Changed += Refresh;
        Refresh();
    }

    private void OnDestroy()
    {
        if (ScoreKeeper.Instance != null) ScoreKeeper.Instance.Changed -= Refresh;
    }

    private void Refresh()
    {
        label.text = $"Score: {ScoreKeeper.Instance.Score}   Stars: {ScoreKeeper.Instance.StarsCollected}";
    }
}
