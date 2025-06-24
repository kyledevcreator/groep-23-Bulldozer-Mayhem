using UnityEngine;

[CreateAssetMenu]
public class PlayerStatistic : ScriptableObject
{
    [Header("Movement")]
    public float gasSpeedBonus;
    public float reverseSpeedBonus;
    public float torqueSpeedBonus;
    [Header("Collision")]
    public float frontStrength;
    public float backStrength;
    public float leftStrength;
    public float rightStrength;

    public float frontPower;
    public float backPower;
    public float leftPower;
    public float rightPower;
    [Header("Physics")]
    public float rotationalDragBonus;
}
