using UnityEngine;

public class FallingObjectManager : MonoBehaviour, IGameObj
{
    [Tooltip("Spawn per second")]
    [SerializeField] private float spawnRate;
    [Tooltip("Pixels per second")]
    [SerializeField] private float startSpeed;
    [Tooltip("Pixels per second")]
    [SerializeField] private float endSpeed;
    
    [SerializeField] private FallingObjectSO[] FObjectSos;
    [SerializeField] private FallingObjectSO[] rareObjectSos;
    
    private FallingObject[] fallingObjects;
    
    private Basket basket;

    public void GStart()
    {
        spawnInterval = 1/spawnRate;
        fallingObjects = GetComponentsInChildren<FallingObject>();

        foreach (FallingObject fallingObject in fallingObjects)
        {
            fallingObject.Despawn();
        }

        basket = GameManager.Instance.basket;
    }

    public void GUpdate()
    {
        TrackDespawning();
        UpdateFOs();
        TrackSpawning();
    }

    #region ChildUpdate
    
    private void UpdateFOs()
    {
        float gameProgress =
            Mathf.InverseLerp(GameManager.Instance.gameTime, 0, GameManager.Instance.timerBoard.timer);
            
        float moveSpeed = Mathf.Lerp(startSpeed, endSpeed, gameProgress);
        
        foreach (FallingObject fallingObject in fallingObjects)
        {
            if(!fallingObject.isActive) continue;
            
            fallingObject.Move(moveSpeed);
            
            if(!fallingObject.isInCatchRange) continue;
            
            Vector2 catchRange = new Vector2(basket.transform.position.x - (basket.halfWidth),
                basket.transform.position.x + (basket.halfWidth));

            if (fallingObject.transform.position.x < catchRange.x ||
                fallingObject.transform.position.x > catchRange.y) continue;
            
            GameManager.Instance.ItemCaught(fallingObject.FallingObjectSO);
            
            fallingObject.Despawn();
        }
    }
    
    private float timer = 0;
    private float spawnInterval;
    
    private void TrackSpawning()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            timer = 0;
            SpawnFO();
        }
    }
    private void TrackDespawning()
    {
        foreach (FallingObject fallingObject in fallingObjects)
        {
            if (!fallingObject.isActive) continue;

            if (fallingObject.hasFallen)
            {
                fallingObject.Despawn();
                GameManager.Instance.ItemDropped(fallingObject.FallingObjectSO);
            }
        }
    }
    private void SpawnFO()
    {
        foreach (FallingObject fallingObject in fallingObjects)
        {
            if (!fallingObject.isSpawnable) continue;
                
            float random =  Random.Range(0f, 1f);
            FallingObjectSO SO;
            
            if(random <= .1f)
                SO = rareObjectSos[Random.Range(0, rareObjectSos.Length)];
            else
                SO = FObjectSos[Random.Range(0, FObjectSos.Length)];
            
            fallingObject.Spawn(Random.Range(0, Screen.width), SO);
            break;
        }
    }
    
    #endregion
    
    
}
