using UnityEngine;

public partial class GameManager
{
    public void ItemCaught(FallingObjectSO fallingObject)
    {
        Debug.Log($"Caught {fallingObject.name}");
        
        scoreBoard.AddScore(fallingObject.value);
    }
}
