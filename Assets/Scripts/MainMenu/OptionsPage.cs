using UnityEngine;
using UnityEngine.UI;

public class OptionsPage : Page
{
    [SerializeField] private Button backButton;

    [SerializeField] private Slider audioSlider;
    
    protected override void Initialize()
    {
        backButton.onClick.AddListener((() =>
        {
            GoToPage<MainMenuPage>();
        }));
        
        audioSlider.onValueChanged.AddListener((arg0 =>
        {
            AudioManager.Instance.SetMasterVolume(arg0);
        }));
        
        base.Initialize();
    }

    public override void Show()
    {
        base.Show();
        
        audioSlider.value = AudioManager.Instance.masterVolume;
    }
}
