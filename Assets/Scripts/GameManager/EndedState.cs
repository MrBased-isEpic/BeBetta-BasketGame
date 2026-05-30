public class EndedState : IGameState
{
    public void Enter()
    {
        GameManager.Instance.pageManager.GoToPage<GameOverPage>();
    }

    public void Update()
    {
    }
}
