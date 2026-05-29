using UnityEngine;
using UnityEngine.UI;

public class FallingObject : MonoBehaviour
{
    [SerializeField] private RectTransform _visual;
    [SerializeField] private Image _image;
    public bool hasFallen => (transform.position.y <= 272);
    
    private FallingObjectSO FallingObjectSO;
    
    public void Spawn(float xPos, FallingObjectSO fallingObjectSo)
    {
        gameObject.SetActive(true);
        FallingObjectSO = fallingObjectSo;
        
        _image.sprite = FallingObjectSO.sprite;
        
        float halfWidth = (_visual.rect.width / 2) + GameManager.Instance.ScreenEdgePadding;
        xPos = Mathf.Clamp(xPos, halfWidth, Screen.width - halfWidth);
        
        transform.position = new Vector3(xPos, Screen.height + _visual.rect.height/2, transform.position.z);
    }

    public void Despawn()
    {
        gameObject.SetActive(false);
    }
    
    public void Move(float speed)
    {
        transform.position -= new Vector3(0, speed * Time.deltaTime, 0);
    }
}
