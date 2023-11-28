using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

// TurnStart: 1. determine who goes first and second; 2. deal cards
// Player1 & Player2: players make their move
// TurnEnd: 1. determine whether the game continues: no hands left; 
public enum GameState { GameStart, TurnStart, Player1, Player2, TurnEnd, GameEnd }

public class TurnBasedSystem : MonoBehaviour
{
    public Player player1;
    public Player player2;
    public Player[] playerActionOrder;
    
    public int[] publicCards;

    public GameState state;
    
    public int turnCount;
    
    public void Start()
    {
        turnCount = 1;
        playerActionOrder = new Player[2];
        
        state = GameState.GameStart;
        StartCoroutine(GameStart());
    }

    private IEnumerator GameStart()
    {
        // Randomly decide who goes first
        var playerOrderDecider = Random.Range(0,1);

        if (playerOrderDecider <= 0.5f)
        {
            SetPlayerOrder(player1, player2);
        }
        else
        {
            SetPlayerOrder(player2, player1);
        }
        
        yield return new WaitForSeconds(3f);

        state = GameState.TurnStart;
        StartCoroutine(TurnStart());
    }

    private void SetPlayerOrder(Player playerFirst, Player playerSecond)
    {
        playerActionOrder[0] = playerFirst;
        playerActionOrder[1] = playerSecond;
    }

    private IEnumerator TurnStart()
    {
        // Refill public cards
        // Refill private cards
        // Determine which player goes first
        
        yield return new WaitForSeconds(3f);

        state = GameState.Player1;
        StartCoroutine(Player1Turn());
    }

    private IEnumerator Player1Turn()
    {
        // Form fraction or erase a mark on number line
        
        yield return new WaitForSeconds(3f);

        state = GameState.Player2;
        StartCoroutine(Player2Turn());
    }

    private IEnumerator Player2Turn()
    {
        // Form fraction or erase a mark on number line
        
        yield return new WaitForSeconds(3f);

        state = GameState.TurnEnd;
        StartCoroutine(TurnEnd());
    }

    private IEnumerator TurnEnd()
    {
        // Determine whether the game continues

        yield return new WaitForSeconds(1f);

        if (true) // the game continues
        {
            turnCount++;
            
            state = GameState.TurnStart;
            StartCoroutine(TurnStart());
        }
        else
        {
            state = GameState.GameEnd;
        }
    }
}
