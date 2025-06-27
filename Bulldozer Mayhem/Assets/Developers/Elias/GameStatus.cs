using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameStatus : ScriptableObject
{
    [HideInInspector] public enum RoundType
    {
        standard,
        ice,
    };
    public int currentRound;
    public int player1Wins, player2Wins;
    public RoundType roundType;
    public string player1Name, player2Name;
}
