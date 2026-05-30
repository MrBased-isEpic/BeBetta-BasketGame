using UnityEngine;
using UnityEngine.UI;

public class GameOverPage : Page
{
    [SerializeField] private Button retryButton;
    [SerializeField] private Button quitButton;

    protected override void Initialize()
    {
        base.Initialize();
        
        retryButton.onClick.AddListener((() =>
        {
            GameManager.Instance.TransitionTo(GameStateType.Setup);
        }));

        quitButton.onClick.AddListener((() =>
        {
    
        }));
    }
}
