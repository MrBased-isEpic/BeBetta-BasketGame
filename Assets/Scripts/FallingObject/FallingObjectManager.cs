using UnityEngine;

public class FallingObjectManager : MonoBehaviour, IGameObj
{
    [SerializeField] private float spawnRate;
    [SerializeField] private float moveSpeed;

    [SerializeField] private FallingObjectSO[] FObjectSos;
    
    private FallingObject[] fallingObjects;

    public void GStart()
    {
        spawnInterval = 1/spawnRate;
        fallingObjects = GetComponentsInChildren<FallingObject>();
    }

    public void GUpdate()
    {
        TrackDespawning();
        UpdateFOs();
        TrackSpawning();
    }

    #region ChildManagement
    
    private void UpdateFOs()
    {
        foreach (FallingObject fallingObject in fallingObjects)
        {
            if(!fallingObject.gameObject.activeSelf) continue;
            
            fallingObject.Move(moveSpeed);
        }
    }
    
    private int activeFallingObjects;
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
                activeFallingObjects--;
                fallingObject.Despawn();
            }
        }
    }
    private void SpawnFO()
    {
        if (activeFallingObjects == fallingObjects.Length) return;
            
        activeFallingObjects++;

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
