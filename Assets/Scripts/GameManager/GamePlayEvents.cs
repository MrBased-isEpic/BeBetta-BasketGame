using System.Collections.Generic;
using UnityEngine;

public partial class GameManager
{
    //private List<EFFECT> activeEffects = new List<EFFECT>();
    
    public void ItemCaught(FallingObjectSO fallingObject)
    {
        //Debug.Log($"Caught {fallingObject.name}");
        scoreBoard.AddScore(basket.isDoubleScore ? fallingObject.value * 2 : fallingObject.value);

        if (fallingObject.effect == EFFECT.None)
        {
            AudioManager.Instance.PlaySFX(pointScoredSFX);
            return;
        }

        if (fallingObject.effect == EFFECT.Damage && !basket.isInvincible)
        {
            liveBoard.RemoveAllLife();
            
            AudioManager.Instance.PlaySFX(lifeLostSFX);

            explosionObject.transform.position = basket._basketVisual.transform.position;
            explosionAnimator.Play();
            
            TransitionTo(GameStateType.Ended);
        }

        switch (fallingObject.effect)
        {
            case EFFECT.Invincible:
                TurnOnInvincibility();
                break;
            case EFFECT.DoubleScore:
                TurnOnDoubleScore();
                break;
        }
        
    }

    public void ItemDropped(FallingObjectSO fallingObject)
    {
        if (fallingObject.effect != EFFECT.None || basket.isInvincible) return;

        bool isAlive = liveBoard.RemoveLife();
        
        AudioManager.Instance.PlaySFX(lifeLostSFX);
            
        if (!isAlive)
            TransitionTo(GameStateType.Ended);
    }

    public void PauseButtonClicked()
    {
        TransitionTo(GameStateType.Paused);
    }
    
    public void GameOver()
    {
        TransitionTo(GameStateType.Ended);
    }

    public void TurnOnInvincibility()
    {
        basket.GiveInvincible(10);
        invincibilityImage.Enable();
        AudioManager.Instance.PlaySFX(invincibilityBeginSFX);
    }

    public void TurnOnDoubleScore()
    {
        basket.GiveDoubleScore(10);
        twoXImage.Enable();
        AudioManager.Instance.PlaySFX(doubleScoreBeginSFX);
    }

    public void DoubleScoreRanOut()
    {
        twoXImage.Disable();
        AudioManager.Instance.PlaySFX(doubleScoreEndSFX);
    }

    public void InvincibilityRanOut()
    {
        invincibilityImage.Disable();
        AudioManager.Instance.PlaySFX(invincibilityEndSFX);
    }
}
