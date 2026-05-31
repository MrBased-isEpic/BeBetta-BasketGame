using System.Collections;
using UnityEngine;

public class EndedState : IGameState
{
    public void Enter()
    {
        GameManager.Instance.StartCoroutine(WaitBeforeEndScreen());
    }

    public void Update()
    {
    }

    private IEnumerator WaitBeforeEndScreen()
    {
        yield return new WaitForSeconds(2f);
        GameManager.Instance.pageManager.GoToPage<GameOverPage>();
    }
}
