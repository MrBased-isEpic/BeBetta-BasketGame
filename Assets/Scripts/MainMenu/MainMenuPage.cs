using UnityEngine;
using UnityEngine.UI;

public class MainMenuPage : Page
{
    [SerializeField] AudioClip music;
    
    [SerializeField] private Button playButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button quitButton;

    protected override void Initialize()
    {
        playButton.onClick.AddListener((() =>
        {
            SceneFlow.Instance.LoadGameScene();
        }));
        
        optionsButton.onClick.AddListener((() =>
        {
            GoToPage<OptionsPage>();
        }));
        
        quitButton.onClick.AddListener((() =>
        {
            Application.Quit();
        }));
        
        AudioManager.Instance.PlayMusic(music);
        
        base.Initialize();
    }
}
