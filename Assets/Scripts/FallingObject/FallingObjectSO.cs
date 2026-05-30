using UnityEngine;

[CreateAssetMenu(fileName = "FallingObjectSO", menuName = "FallingObjectSO")]
public class FallingObjectSO : ScriptableObject
{
    public int value;
    public EFFECT effect;
    public Sprite sprite;
}

public enum EFFECT
{
    None,
    Damage,
    Invincible,
    DoubleScore
}
