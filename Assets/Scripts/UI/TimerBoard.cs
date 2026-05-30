 using TMPro;
 using UnityEngine;

public class TimerBoard : MonoBehaviour, IGameObj
{
    [SerializeField] private TextMeshProUGUI timerText;

    private float timer;
    
    public void GStart()
    {
        timer = GameManager.Instance.gameTime;
    }

    public void GUpdate()
    {
        timer -= Time.deltaTime;
        
        int minutes = (int)(timer / 60);
        int seconds = (int)(timer % 60);
        
        timerText.text = minutes.ToString("00") + ":" + seconds.ToString("00");

        if (timer <= 0)
        {
            GameManager.Instance.GameOver();
            timerText.text = "00:00";
        }

    }
}
