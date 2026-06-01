using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FallingObject : MonoBehaviour
{
    [SerializeField] private RectTransform _visual;
    [SerializeField] private Image _image;
    
    public float halfWidth {get; private set; }
    public bool hasFallen => transform.position.y <= catchRange.x;
    public bool isInCatchRange => (transform.position.y <= catchRange.y && transform.position.y > catchRange.x);

    private Vector2 catchRange = new Vector2(268, 360);
    public FallingObjectSO FallingObjectSO {get; private set; }

    public bool isActive;
    public bool isSpawnable;
    
    private Coroutine animationRoutine;

    public void Spawn(float xPos, FallingObjectSO fallingObjectSo)
    {
        gameObject.SetActive(true);
        
        FallingObjectSO = fallingObjectSo;
        
        _image.sprite = FallingObjectSO.sprite;
        
        halfWidth = (_visual.rect.width / 2) + GameManager.Instance.ScreenEdgePadding;
        xPos = Mathf.Clamp(xPos, halfWidth, Screen.width - halfWidth);
        
        transform.position = new Vector3(xPos, 1619, transform.position.z);
        
        if (animationRoutine != null)
        {
            StopCoroutine(animationRoutine);
            _image.color = Color.white;
        }

        animationRoutine = StartCoroutine(SpawnIn());

        isActive = true;
        isSpawnable = false;
    }

    public void Despawn()
    {
        StartCoroutine(DeSpawnOut());
    }
    
    public void Move(float speed)
    {
        transform.position -= new Vector3(0, speed * Time.deltaTime, 0);
    }

    private IEnumerator SpawnIn()
    {
        _image.color = Color.clear;
        yield return StartCoroutine(Animations.LerpColor(_image, Color.white, .3f));
    }

    private IEnumerator DeSpawnOut()
    {
        isActive = false;
        
        StartCoroutine(Animations.LerpColor(_image, Color.clear, .2f));
        yield return StartCoroutine(
            Animations.MoveTransform(transform, 
                transform.position - new Vector3(0, 100, 0), .2f));
        
        gameObject.SetActive(false);
        
        isSpawnable = true;
    }
}
