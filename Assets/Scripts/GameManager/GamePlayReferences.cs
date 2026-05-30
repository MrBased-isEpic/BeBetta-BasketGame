using UnityEngine;

public partial class GameManager
{
    public InputManager inputManager;
    public Basket basket;
    public FallingObjectManager fallingObjectManager;
    
    [Space]
    public ScoreBoard scoreBoard;
    public LiveBoard liveBoard;

    [Space]
    public float ScreenEdgePadding;
}
