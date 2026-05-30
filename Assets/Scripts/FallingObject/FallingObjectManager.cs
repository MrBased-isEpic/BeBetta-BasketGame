using UnityEngine;

public class FallingObjectManager : MonoBehaviour, IGameObj
{
    [Tooltip("Spawn per second")]
    [SerializeField] private float spawnRate;
    [Tooltip("Pixels per second")]
    [SerializeField] private float moveSpeed;

    
    [SerializeField] private FallingObjectSO[] FObjectSos;
    
    private FallingObject[] fallingObjects;
    
    private Basket basket;

    public void GStart()
    {
        spawnInterval = 1/spawnRate;
        fallingObjects = GetComponentsInChildren<FallingObject>();

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
        foreach (FallingObject fallingObject in fallingObjects)
        {
            if(!fallingObject.gameObject.activeSelf) continue;
            
            fallingObject.Move(moveSpeed);
            
            if(!fallingObject.isInCatchRange) continue;
            
            Vector2 catchRange = new Vector2(basket.transform.position.x - (basket.halfWidth - fallingObject.halfWidth),
                basket.transform.position.x + (basket.halfWidth - fallingObject.halfWidth));

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
            if (!fallingObject.gameObject.activeSelf) continue;

            if (fallingObject.hasFallen)
            {
                fallingObject.Despawn();
            }
        }
    }
    private void SpawnFO()
    {
        foreach (FallingObject fallingObject in fallingObjects)
        {
            if (fallingObject.gameObject.activeSelf) continue;
                
            FallingObjectSO SO = FObjectSos[Random.Range(0, FObjectSos.Length)];
            fallingObject.Spawn(Random.Range(0, Screen.width), SO);
            break;
        }
    }
    
    #endregion
    
    
}
