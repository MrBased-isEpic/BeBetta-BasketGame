using UnityEngine;
using UnityEngine.UI;

public class OptionsPage : Page
{
    [SerializeField] private Button backButton;
    
    protected override void Initialize()
    {
        backButton.onClick.AddListener((() =>
        {
            GoToPage<MainMenuPage>();
        }));
        
        base.Initialize();
    }
}
