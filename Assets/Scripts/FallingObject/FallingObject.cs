using UnityEngine;
using UnityEngine.UI;

public class FallingObject : MonoBehaviour
{
    [SerializeField] private RectTransform _visual;
    [SerializeField] private Image _image;
    
    public float halfWidth {get; private set; }
    public bool hasFallen => transform.position.y <= catchRange.x;
    public bool isInCatchRange => (transform.position.y <= catchRange.y && transform.position.y > catchRange.x);

    private Vector2 catchRange = new Vector2(225, 332);


    public FallingObjectSO FallingObjectSO {get; private set; }

    public void Spawn(float xPos, FallingObjectSO fallingObjectSo)
    {
        gameObject.SetActive(true);
        FallingObjectSO = fallingObjectSo;
        
        _image.sprite = FallingObjectSO.sprite;
        
        halfWidth = (_visual.rect.width / 2) + GameManager.Instance.ScreenEdgePadding;
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
