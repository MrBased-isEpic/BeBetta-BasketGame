public class PausedState : IGameState
{
    public void Enter()
    {
        GameManager.Instance.pageManager.GoToPage<PausePage>();
    }

    public void Update()
    {
    }
}
