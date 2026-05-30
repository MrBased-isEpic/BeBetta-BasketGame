using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LiveBoard : MonoBehaviour
{
    [SerializeField] private Color onColor;
    [SerializeField] private Color offColor;

    [SerializeField] private Image[] lifeImages;
    private int lives;

    public void Reset()
    {
        lives = lifeImages.Length;

        foreach (Image image in lifeImages)
        {
            image.color = onColor;
        }
    }

    public bool RemoveLife()
    {
        lives--;

        for (int index = 0; index < lifeImages.Length; index++)
        {
            lifeImages[index].color = index > lives - 1 ? offColor : onColor;
        }
        
        return (lives > 0);
    }
}
