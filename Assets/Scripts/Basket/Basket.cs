using UnityEngine;

public class Basket : MonoBehaviour, IGameObj
{
    private float halfWidth;
    [SerializeField] private RectTransform _basketVisual;

    public void GStart()
    {
        halfWidth = _basketVisual.rect.width / 2 + GameManager.Instance.ScreenEdgePadding;
    }
    
    public void GUpdate()
    {
        float xPos = InputManager.Instance.xPos;
        
        xPos = Mathf.Clamp(xPos, halfWidth, Screen.width - halfWidth);
        transform.position = new Vector3(xPos, transform.position.y, transform.position.z);
    }
}
