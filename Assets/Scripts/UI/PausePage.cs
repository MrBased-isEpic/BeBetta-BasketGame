using UnityEngine;
using UnityEngine.UI;

public class PausePage : Page
{
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button retryButton;
    [SerializeField] private Button quitButton;

    protected override void Initialize()
    {
        base.Initialize();
        
        resumeButton.onClick.AddListener((() =>
        {
            GameManager.Instance.TransitionTo(GameStateType.Playing);
        }));
        
        retryButton.onClick.AddListener((() =>
        {
            GameManager.Instance.TransitionTo(GameStateType.Setup);
        }));

        quitButton.onClick.AddListener((() =>
        {
            SceneFlow.Instance.LoadMainMenuScene();
        }));
    }
}
