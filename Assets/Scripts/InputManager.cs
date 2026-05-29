using UnityEngine;

public class InputManager : MonoBehaviour, IGameObj
{
    
    #region Singleton
    
    public static InputManager Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }
    
    #endregion
    
    #region Properties

    public float xPos => _xPos;
    
    #endregion

    public int basketInputCutoffHeight = 540;

    private float _xPos;


    public void GStart()
    {
        _xPos = Screen.width / 2;
    }
    
    public void GUpdate()
    {
        if (!Input.GetMouseButton(0)) return;

        if(Input.mousePosition.y >= basketInputCutoffHeight) return;
        
        _xPos = Input.mousePosition.x;
    }
}
