using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;
using UnityEngine.Serialization;

public class PlayerManager : NetworkBehaviour
{
    public GameObject cardPrefab;
    public GameObject playerArea;
    public GameObject opponentArea;
    public GameObject publicArea;
    
    [SyncVar]
    public List<int> deck = new List<int>();
    
    [SyncVar]
    public int deckIndex;

    public override void OnStartClient()
    {
        base.OnStartClient();

        playerArea = GameObject.Find("Player Area");
        opponentArea = GameObject.Find("Opponent Area");
        publicArea = GameObject.Find("Public Area");
    }

    [Server]
    public override void OnStartServer()
    {
        base.OnStartServer();

        GenerateDeck();
        ShuttleDeck();
    }

    private void GenerateDeck()
    {
        for (var cardValue = 1; cardValue <= 13; cardValue++)
        {
            for (var cardCount = 0; cardCount < 4; cardCount++)
            {
                deck.Add(cardValue);
            }
        }
    }
    
    private void ShuttleDeck()
    {
        for (var i = 0; i < 52; i++)
        {
            var swapIndex = Random.Range(0, 52);
            (deck[i], deck[swapIndex]) = (deck[swapIndex], deck[i]);
        }
    }

    [Command]
    public void CmdDealCard()
    {
        var card = Instantiate(cardPrefab, new Vector2(), Quaternion.identity);
        NetworkServer.Spawn(card);
        RpcDealCard(card);
    }

    [ClientRpc]
    private void RpcDealCard(GameObject card)
    {
        card.GetComponent<Card>().id = deckIndex++;
        
        if (isOwned)
        {
            card.transform.SetParent(playerArea.transform, false);
            card.GetComponent<FlipCards>().FlipCard();
        }
        else
        {
            card.transform.SetParent(opponentArea.transform, false);
        }
    }

    [Command]
    public void CmdPlayCards(GameObject playedCard)
    {
        RpcRenderCards(playedCard, "Played");
    }

    [ClientRpc]
    private void RpcRenderCards(GameObject newCard, string type)
    {
        switch (type)
        {
            case ("Deal"):
                
                if (isOwned)
                {
                    newCard.transform.SetParent(playerArea.transform, false);
                    newCard.GetComponent<FlipCards>().FlipCard();
                }
                else
                {
                    newCard.transform.SetParent(opponentArea.transform, false);
                }
                
                break;
            
            case ("Played"):
                
                newCard.transform.SetParent(publicArea.transform, false);
                
                if (isOwned)
                {
                    newCard.GetComponent<FlipCards>().FlipCard();
                } 
                newCard.GetComponent<FlipCards>().AnimatedFlipCard();
                    
                break;
        }
    }

    [Command]
    public void CmdGetCardValue(int id, GameObject card)
    {
        RpcGetValueById(id, card);
    }

    [ClientRpc]
    private void RpcGetValueById(int id, GameObject card)
    {
        card.GetComponent<Card>().number.text = deck[id].ToString();
    }
    
}
