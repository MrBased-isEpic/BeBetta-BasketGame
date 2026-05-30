using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverPage : Page
{
    [SerializeField] private Button retryButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private TextMeshProUGUI scoreText;

    protected override void Initialize()
    {
        base.Initialize();
        
        retryButton.onClick.AddListener((() =>
        {
            GameManager.Instance.TransitionTo(GameStateType.Setup);
        }));

        quitButton.onClick.AddListener((() =>
        {
            SceneFlow.Instance.LoadMainMenuScene();
        }));
    }

    public override void Show()
    {
        scoreText.text = $"Score : {GameManager.Instance.scoreBoard.score.ToString()}";
        
        base.Show();
    }
}
