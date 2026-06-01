using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TwoXImage : MonoBehaviour
{
    private Vector3 originalPosition;
    private Image visual;
    private Color origianalColor;
    
    private Coroutine animationRoutine;

    private void Awake()
    {
        originalPosition = transform.position;
        visual = GetComponent<Image>(); 
        origianalColor = visual.color;
    }
    
    public void Enable()
    {
        gameObject.SetActive(true);
        
        if (animationRoutine != null)
        {
            StopCoroutine(animationRoutine);
            transform.position = originalPosition;
            visual.color = origianalColor;
        }

        animationRoutine = StartCoroutine(EnableRoutine());
    }

    public void Disable()
    {
        if (!gameObject.activeSelf) return;
        
        if (animationRoutine != null)
        {
            StopCoroutine(animationRoutine);
            transform.position = originalPosition;
            visual.color = origianalColor;
        }
        
        animationRoutine = StartCoroutine(DisableRoutine());
    }
    
    private IEnumerator EnableRoutine()
    {
        transform.position = originalPosition + new Vector3(100, 0, 0);

        visual.color = Color.clear;
        
        StartCoroutine(Animations.LerpColor(visual, origianalColor, .3f));
        
        yield return StartCoroutine(
            Animations.MoveTransform(transform, originalPosition, .3f, Eases.EaseOutCubic));
    }

    private IEnumerator DisableRoutine()
    {
        StartCoroutine(Animations.LerpColor(visual, Color.clear, .3f));
        
        yield return StartCoroutine(
            Animations.MoveTransform(transform, 
                originalPosition + new Vector3(100, 0, 0), .3f, Eases.EaseInCubic));
        
        gameObject.SetActive(false);
    }
}
