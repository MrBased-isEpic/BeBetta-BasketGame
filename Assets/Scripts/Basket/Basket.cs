using System.Collections;
using UnityEngine;

public class Basket : MonoBehaviour, IGameObj
{
    public float halfWidth {get; private set; }
    public RectTransform _basketVisual;
    
    private Coroutine animationRoutine;
    
    #region Powerups
    
    public bool isInvincible => invincibleTimer > 0f;
    public bool isDoubleScore => doubleScoreTimer > 0f;

    private float invincibleTimer;
    private float doubleScoreTimer;
    
    private void UpdatePowerupTimers()
    {
        if (invincibleTimer > 0)
        {
            invincibleTimer -= Time.deltaTime;
            
            if(invincibleTimer <= 0)
                GameManager.Instance.InvincibilityRanOut();
        }
        
        if (doubleScoreTimer > 0)
        {
            doubleScoreTimer -= Time.deltaTime;
            
            if(doubleScoreTimer <= 0)
                GameManager.Instance.DoubleScoreRanOut();
        }
    }
    
    public void GiveInvincible(float time)
    {
        invincibleTimer = time;
    }
    
    public void GiveDoubleScore(float time)
    {
        doubleScoreTimer = time;
    }
    
    #endregion

    public void GStart()
    {
        halfWidth = _basketVisual.rect.width / 2 + GameManager.Instance.ScreenEdgePadding;
    }
    
    public void GUpdate()
    {
        float xPos = InputManager.Instance.xPos;
        
        xPos = Mathf.Clamp(xPos, halfWidth, Screen.width - halfWidth);
        transform.position = new Vector3(xPos, transform.position.y, transform.position.z);

        UpdatePowerupTimers();
    }

    public void CollectPoint()
    {
        if (animationRoutine != null)
        {
            StopCoroutine(animationRoutine);
            transform.localScale = Vector3.one;
        }
        
        animationRoutine = StartCoroutine(TwitchTransform(transform, .1f, .1f));
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
