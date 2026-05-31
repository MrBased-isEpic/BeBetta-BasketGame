using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ImageAnimation : MonoBehaviour {

    public Sprite[] sprites;
    public int spritePerFrame = 6;

    private int index = 0;
    private Image image;
    private int frame = 0;

    void Awake() {
        image = GetComponent<Image>();
    }

    public Coroutine animation;

    public void Play()
    {
        if(animation != null) StopCoroutine(animation);
        
        animation = StartCoroutine(Animate());
    }

    IEnumerator Animate() 
    {
        image.color = new Color(1, 1, 1, 1);
        
        while (index < sprites.Length)
        {
            yield return null;
            frame++;
            
            if (frame < spritePerFrame) continue;
            image.sprite = sprites [index];
            
            frame = 0;
            index ++;
        }
        
        image.sprite = null;
        image.color = new Color(1, 1, 1, 0);

        animation = null;
    }
}