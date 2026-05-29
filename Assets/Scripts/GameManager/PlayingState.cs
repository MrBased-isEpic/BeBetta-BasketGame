public class PlayingState : IGameState
{
    public void Enter()
    {
    }

    public void Update()
    {
        GameManager manager = GameManager.Instance;
        
        manager.inputManager.GUpdate();
        manager.basket.GUpdate();
        manager.fallingObjectManager.GUpdate();
    }
}
