public class SetupState : IGameState
{
    public void Enter()
    {
        GameManager manager = GameManager.Instance;
        
        manager.inputManager.GStart();
        manager.basket.GStart();
        manager.fallingObjectManager.GStart();
        
        manager.TransitionTo(GameStateType.Playing);
    }

    public void Update() {}
}
