using UnityEngine;

[CreateAssetMenu(fileName = "FallingObjectSO", menuName = "FallingObjectSO")]
public class FallingObjectSO : ScriptableObject
{
    public int value;
    public BONUS bonus;
    public Sprite sprite;
}

public enum BONUS
{
    None,
    Invincible,
    DoubleScore
}
