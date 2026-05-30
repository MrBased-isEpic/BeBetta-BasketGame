using System.Collections.Generic;
using UnityEngine;

public partial class GameManager
{
    //private List<EFFECT> activeEffects = new List<EFFECT>();
    
    public void ItemCaught(FallingObjectSO fallingObject)
    {
        //Debug.Log($"Caught {fallingObject.name}");
        scoreBoard.AddScore(fallingObject.value);

        if (fallingObject.effect == EFFECT.None) return;

        if (fallingObject.effect == EFFECT.Damage)
            liveBoard.RemoveLife();

    }

    public void ItemDropped(FallingObjectSO fallingObject)
    {
        if (fallingObject.effect == EFFECT.Damage) return;

        liveBoard.RemoveLife();
    }
}
