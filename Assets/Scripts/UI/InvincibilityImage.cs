using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class InvincibilityImage : MonoBehaviour
{
    private Vector3 originalPosition;
    private Image visual;
    
    private Coroutine animationRoutine;

    private void Awake()
    {
        originalPosition = transform.position;
        visual = GetComponent<Image>(); 
    }
    
    public void Enable()
    {
        gameObject.SetActive(true);
        
        if (animationRoutine != null)
        {
            StopCoroutine(animationRoutine);
            transform.position = originalPosition;
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
        }
        
        animationRoutine = StartCoroutine(DisableRoutine());
    }
    
    private IEnumerator EnableRoutine()
    {
        transform.position = originalPosition + new Vector3(0, -200, 0);
        
        yield return StartCoroutine(
            Animations.MoveTransform(transform, originalPosition, .3f, Eases.EaseOutCubic));
    }

    private IEnumerator DisableRoutine()
    {
        
        yield return StartCoroutine(
            Animations.MoveTransform(transform, 
                originalPosition + new Vector3(0, -200, 0), .3f, Eases.EaseInCubic));
        
        gameObject.SetActive(false);
    }
}
