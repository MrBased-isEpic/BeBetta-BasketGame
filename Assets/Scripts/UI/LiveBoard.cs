using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LiveBoard : MonoBehaviour
{
    [SerializeField] private Color onColor;
    [SerializeField] private Color offColor;

    [SerializeField] private Image[] lifeImages;
    private int lives;
    
    private Coroutine animationRoutine;

    public void Reset()
    {
        lives = lifeImages.Length;

        foreach (Image image in lifeImages)
        {
            image.color = onColor;
        }
    }

    public bool RemoveLife()
    {
        lives--;
        
        Image changeImage = lifeImages[lives];

        if (animationRoutine != null)
        {
            StopCoroutine(animationRoutine);
            foreach (Image image in lifeImages)
            {
                image.transform.localScale = Vector3.one;
            }
        }
            
        animationRoutine = StartCoroutine(LoseLifeAnim(changeImage));
        
        return (lives > 0);
    }

    public void RemoveAllLife()
    {
        lives = 0;
        
        if (animationRoutine != null)
        {
            StopCoroutine(animationRoutine);
            foreach (Image image in lifeImages)
            {
                image.transform.localScale = Vector3.one;
            }
        }
        
        animationRoutine = StartCoroutine(LoseAllLifeAnim());
    }


    public IEnumerator LoseLifeAnim(Image image)
    {
        StartCoroutine(Animations.LerpColor(image, offColor, .1f));
        yield return StartCoroutine(TwitchTransform(image.transform, .3f, .1f));
    }

    public IEnumerator LoseAllLifeAnim()
    {
        for (int index = lifeImages.Length - 1; index >= 0; index--)
        {
            Image image = lifeImages[index];
            StartCoroutine(Animations.LerpColor(image, offColor, .1f));
            yield return StartCoroutine(TwitchTransform(image.transform, .3f, .1f));
        }
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
