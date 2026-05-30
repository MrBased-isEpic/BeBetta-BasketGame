using UnityEngine;
using UnityEngine.UI;

public partial class GameManager
{
    public PageManager pageManager;
    public InputManager inputManager;
    public Basket basket;
    public FallingObjectManager fallingObjectManager;
    
    [Space]
    public ScoreBoard scoreBoard;
    public LiveBoard liveBoard;
    public TimerBoard timerBoard;
    public Button pauseButton;
    
    [Space]
    public TwoXImage twoXImage;
    public InvincibilityImage invincibilityImage;

    [Space] public float gameTime;
    public float ScreenEdgePadding;
}
