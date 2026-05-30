using UnityEngine;

public class Basket : MonoBehaviour, IGameObj
{
    public float halfWidth {get; private set; }
    [SerializeField] private RectTransform _basketVisual;
    
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

    
}
