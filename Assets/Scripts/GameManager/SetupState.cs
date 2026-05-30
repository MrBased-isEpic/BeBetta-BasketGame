public class SetupState : IGameState
{
    public void Enter()
    {
        GameManager manager = GameManager.Instance;
        
        manager.scoreBoard.Reset();
        manager.liveBoard.Reset();
        
        manager.timerBoard.GStart();
        
        manager.inputManager.GStart();
        manager.basket.GStart();
        manager.fallingObjectManager.GStart();
        
        manager.pageManager.Initialize();
        
        manager.TransitionTo(GameStateType.Playing);
    }

    public void Update() {}
}
