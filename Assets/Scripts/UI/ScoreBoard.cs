using System.Collections;
using TMPro;
using UnityEngine;

public class ScoreBoard : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    
    private Coroutine animationRoutine;

    public int score { get; private set; }

    public void AddScore(int score)
    {
        this.score += score;
        scoreText.text = this.score.ToString("0000");

        if (animationRoutine != null)
        {
            StopCoroutine(animationRoutine);
            scoreText.transform.localScale = Vector3.one;
        }

        animationRoutine = StartCoroutine(TwitchTransform(scoreText.transform, .2f, .1f));
    }

    public void Reset()
    {
        this.score = 0;
        scoreText.text = "0000";
    }
    
    public IEnumerator TwitchTransform(Transform target, float strength = .2f, float duration = 1)
    {
        yield return StartCoroutine(Animations.ScaleTransform(target,
            new Vector3(1+strength, 1-strength, 1), duration/2));
        
        yield return StartCoroutine(Animations.ScaleTransform(target,
            new Vector3(1-strength, 1+strength, 1), duration/2));
        
        yield return StartCoroutine(Animations.ScaleTransform(target,
            Vector3.one, duration, Eases.EaseInCubic));

        animationRoutine = null;
    }
}
