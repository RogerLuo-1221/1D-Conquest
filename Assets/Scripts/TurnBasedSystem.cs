using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { TurnStart, Player1, Player2 }

public class TurnBasedSystem : MonoBehaviour
{
    public PlayerManager player1;
    public PlayerManager player2;

    [SerializeField] private GameState _state;
    
    private int _turnCount;
    
    public void Start()
    {
        _state = GameState.TurnStart;
        StartCoroutine(SetUpTurn());
    }

    private IEnumerator SetUpTurn()
    {
        yield return null;
    }
}
