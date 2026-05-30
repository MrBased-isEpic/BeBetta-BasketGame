public class PlayingState : IGameState
{
    public void Enter()
    {
        GameManager manager = GameManager.Instance;
        manager.pageManager.GoToPage<GameplayPage>();
        
        manager.pauseButton.onClick.RemoveAllListeners();
        manager.pauseButton.onClick.AddListener(manager.PauseButtonClicked);
    }

    public void Update()
    {
        GameManager manager = GameManager.Instance;
        
        manager.inputManager.GUpdate();
        manager.basket.GUpdate();
        manager.fallingObjectManager.GUpdate();
        manager.timerBoard.GUpdate();
    }
}
