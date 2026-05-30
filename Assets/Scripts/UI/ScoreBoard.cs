using TMPro;
using UnityEngine;

public class ScoreBoard : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;

    public int score { get; private set; }

    public void AddScore(int score)
    {
        this.score += score;
        scoreText.text = this.score.ToString("0000");
    }

    public void Reset()
    {
        this.score = 0;
        scoreText.text = "0000";
    }
}
